$(document).ready(function() {
    class DragDropManager {
        constructor() {
            this.availableItems = $('#availableItems');
            this.dropZones = $('.drop-zone');
            this.conflictDialog = $('#conflictDialog');
            this.pendingDrop = null;

            this.initializeItems();
            this.initializeDragDrop();
            this.initializeConflictDialog();
        }

        initializeItems() {
            const items = [
                { width: 1, height: 1, type: '1x1', image: 'https://placehold.co/80x80/007bff/white?text=1x1' },
                { width: 1, height: 2, type: '1x2', image: 'https://placehold.co/80x160/007bff/white?text=1x2' },
                { width: 2, height: 1, type: '2x1', image: 'https://placehold.co/160x80/007bff/white?text=2x1' },
                { width: 2, height: 2, type: '2x2', image: 'https://placehold.co/160x160/007bff/white?text=2x2' },
                { width: 3, height: 2, type: '3x2', image: 'https://placehold.co/240x160/007bff/white?text=3x2' }
            ];

            items.forEach(item => {
                this.createDraggableItem(item.image, item.type, item.width, item.height)
                    .appendTo(this.availableItems);
            });
        }

        initializeDragDrop() {
            $('.draggable').draggable({
                helper: 'clone',
                cursor: 'move',
                opacity: 0.7,
                revert: 'invalid',
                grid: [80, 80],
                start: (event, ui) => {
                    $(ui.helper)
                        .addClass('dragging')
                        .css({
                            width: event.target.offsetWidth,
                            height: event.target.offsetHeight
                        });
                }
            });

            this.dropZones.droppable({
                accept: '.draggable',
                tolerance: 'intersect',
                hoverClass: 'drop-hover',
                drop: (event, ui) => this.handleDrop(event, ui)
            });
        }

        initializeConflictDialog() {
            this.conflictDialog.dialog({
                autoOpen: false,
                modal: true,
                width: 400,
                closeOnEscape: true,
                resizable: false,
                buttons: {
                    Replace: () => {
                        if (this.pendingDrop) {
                            const { conflicts } = this.pendingDrop;
                            $(conflicts).each((_, item) => $(item).remove());
                            this.finalizeDrop();
                        }
                        this.conflictDialog.dialog('close');
                    },
                    'Keep Both': () => {
                        if (this.pendingDrop) {
                            this.finalizeDrop();
                        }
                        this.conflictDialog.dialog('close');
                    },
                    Cancel: () => {
                        this.pendingDrop = null;
                        this.conflictDialog.dialog('close');
                    }
                },
                close: () => {
                    this.pendingDrop = null;
                }
            });
        }

        getGridPosition(event, ui, dropZone) {
            const dropRect = dropZone[0].getBoundingClientRect();
            const helperRect = ui.helper[0].getBoundingClientRect();
            
            // Calculate relative position
            const relX = helperRect.left - dropRect.left;
            const relY = helperRect.top - dropRect.top;
            
            // Snap to grid
            const col = Math.round(relX / 80);
            const row = Math.round(relY / 80);
            
            return {
                col: Math.max(0, Math.min(col, 5)),
                row: Math.max(0, Math.min(row, 5))
            };
        }

        handleDrop(event, ui) {
            const dropZone = $(event.target);
            const draggedItem = ui.draggable;
            
            const width = parseInt(draggedItem.data('width'));
            const height = parseInt(draggedItem.data('height'));
            
            const pos = this.getGridPosition(event, ui, dropZone);
            
            // Check bounds
            if (!this.isWithinBounds(dropZone, pos.row, pos.col, width, height)) {
                return false;
            }

            // Get existing items
            const existingItems = dropZone.find('.draggable').not(draggedItem);
            const conflicts = this.findConflicts(existingItems, pos.row, pos.col, width, height);

            if (conflicts.length > 0) {
                const conflictingNames = conflicts.map(item => {
                    const $item = $(item);
                    return `${$item.data('width')}x${$item.data('height')} at (${$item.data('row')},${$item.data('col')})`;
                }).join(', ');

                this.showConflictModal(draggedItem, dropZone, pos.row, pos.col, width, height, conflicts, conflictingNames);
                return false;
            }

            this.finalizeItemPlacement(draggedItem, dropZone, pos.row, pos.col, width, height);
            return true;
        }

        findConflicts(existingItems, newRow, newCol, newWidth, newHeight) {
            const conflicts = [];
            
            existingItems.each((_, item) => {
                const $item = $(item);
                const itemRow = parseInt($item.data('row'));
                const itemCol = parseInt($item.data('col'));
                const itemWidth = parseInt($item.data('width'));
                const itemHeight = parseInt($item.data('height'));

                if (!isNaN(itemRow) && !isNaN(itemCol) && !isNaN(itemWidth) && !isNaN(itemHeight)) {
                    // Check if rectangles overlap
                    if (this.checkOverlap(
                        newRow, newCol, newWidth, newHeight,
                        itemRow, itemCol, itemWidth, itemHeight
                    )) {
                        conflicts.push(item);
                    }
                }
            });

            return conflicts;
        }

        checkOverlap(row1, col1, width1, height1, row2, col2, width2, height2) {
            return !(
                col1 >= col2 + width2 ||    // rect1 is completely to the right
                col1 + width1 <= col2 ||    // rect1 is completely to the left
                row1 >= row2 + height2 ||   // rect1 is completely below
                row1 + height1 <= row2      // rect1 is completely above
            );
        }

        isWithinBounds(dropZone, row, col, width, height) {
            return row >= 0 && col >= 0 && row + height <= 6 && col + width <= 6;
        }

        showConflictModal(draggedItem, dropZone, row, col, width, height, conflicts, conflictingNames) {
            $('#conflictMessage').text(`Conflict detected with: ${conflictingNames}`);
            this.pendingDrop = { draggedItem, dropZone, row, col, width, height, conflicts };
            this.conflictDialog.dialog('open');
        }

        finalizeDrop() {
            if (!this.pendingDrop) return;

            const { draggedItem, dropZone, row, col, width, height } = this.pendingDrop;
            this.finalizeItemPlacement(draggedItem, dropZone, row, col, width, height);
            this.pendingDrop = null;
        }

        finalizeItemPlacement(item, dropZone, row, col, width, height) {
            const newItem = item.parent().is(this.availableItems) ? 
                this.createDraggableItem(
                    item.find('img').attr('src'),
                    item.data('type'),
                    width,
                    height
                ) : item;

            // Calculate position with grid alignment
            const left = col * 80;
            const top = row * 80;

            newItem.css({
                position: 'absolute',
                left: `${left}px`,
                top: `${top}px`,
                width: `${width * 80 - 10}px`,
                height: `${height * 80 - 10}px`,
                margin: '5px'
            }).data({
                row: row,
                col: col,
                width: width,
                height: height
            });

            dropZone.append(newItem);
            
            if (item.parent().is(this.availableItems)) {
                newItem.draggable({
                    helper: 'clone',
                    cursor: 'move',
                    opacity: 0.7,
                    revert: 'invalid',
                    grid: [80, 80],
                    start: (event, ui) => {
                        $(ui.helper)
                            .addClass('dragging')
                            .css({
                                width: event.target.offsetWidth,
                                height: event.target.offsetHeight
                            });
                    }
                });
            }
        }

        createDraggableItem(imgSrc, type, width, height) {
            const item = $('<div>')
                .addClass('draggable')
                .attr('draggable', true)
                .data({
                    type: type,
                    width: width,
                    height: height
                });

            $('<img>')
                .attr({
                    src: imgSrc,
                    alt: `${width}x${height} Image`
                })
                .appendTo(item);

            $('<span>')
                .addClass('size-label')
                .text(`${width}x${height}`)
                .appendTo(item);

            if (this.availableItems.find(item).length) {
                item.css({
                    width: `${width * 80 - 10}px`,
                    height: `${height * 80 - 10}px`,
                    margin: '5px'
                });
            }

            return item;
        }
    }

    new DragDropManager();
});
