class DragDropManager {
    constructor() {
        this.dropZones = Array.from(document.querySelectorAll('.drop-zone'));
        this.availableItems = document.getElementById('available-items');
        this.removeZone = document.getElementById('remove-zone');
        this.conflictModal = document.getElementById('conflict-modal');
        this.conflictMessage = document.getElementById('conflict-message');
        this.gridState = new Map();
        this.resolutionHandlersSet = false;

        this.initializeGridState();
        this.initializeEventListeners();
        this.createInitialItems();
    }

    initializeGridState() {
        this.dropZones.forEach(zone => {
            const rows = parseInt(zone.dataset.rows) || 6;
            const cols = parseInt(zone.dataset.cols) || 6;
            const grid = Array(rows).fill(null).map(() => Array(cols).fill(null));
            this.gridState.set(zone.id, grid);
        });
    }

    initializeEventListeners() {
        this.dropZones.forEach(zone => {
            zone.addEventListener('dragover', this.handleDragOver.bind(this));
            zone.addEventListener('drop', this.handleDrop.bind(this));
            zone.addEventListener('dragenter', this.handleDragEnter.bind(this));
            zone.addEventListener('dragleave', this.handleDragLeave.bind(this));
        });

        if (this.removeZone) {
            this.removeZone.addEventListener('dragover', this.handleDragOver.bind(this));
            this.removeZone.addEventListener('dragenter', e => {
                e.preventDefault();
                this.removeZone.classList.add('drag-over');
            });
            this.removeZone.addEventListener('dragleave', () => {
                this.removeZone.classList.remove('drag-over');
            });
            this.removeZone.addEventListener('drop', this.handleRemoveDrop.bind(this));
        }
    }

    createInitialItems() {
        const items = [
            { src: 'https://placehold.co/80x80/4CAF50/white?text=1x1', type: 'image', width: 1, height: 1 },
            { src: 'https://placehold.co/80x80/2196F3/white?text=1x1', type: 'image', width: 1, height: 1 },
            { src: 'https://placehold.co/160x80/9C27B0/white?text=2x1', type: 'image', width: 2, height: 1 },
            { src: 'https://placehold.co/160x80/FF9800/white?text=2x1', type: 'image', width: 2, height: 1 },
            { src: 'https://placehold.co/80x160/F44336/white?text=1x2', type: 'image', width: 1, height: 2 },
            { src: 'https://placehold.co/80x160/009688/white?text=1x2', type: 'image', width: 1, height: 2 },
            { src: 'https://placehold.co/160x160/795548/white?text=2x2', type: 'image', width: 2, height: 2 },
            { src: 'https://placehold.co/160x160/607D8B/white?text=2x2', type: 'image', width: 2, height: 2 },
            { src: 'https://placehold.co/240x160/E91E63/white?text=3x2', type: 'image', width: 3, height: 2 },
            { src: 'https://placehold.co/240x160/673AB7/white?text=3x2', type: 'image', width: 3, height: 2 }
        ];

        // Clear existing items
        this.availableItems.innerHTML = '';

        // Create and add new items
        items.forEach(item => {
            const dragItem = this.createDraggableItem(item.src, item.type, item.width, item.height);
            
            // Set initial size for items in the available items container
            dragItem.style.width = `${item.width * 80 - 10}px`;
            dragItem.style.height = `${item.height * 80 - 10}px`;
            dragItem.style.margin = '5px';
            
            this.availableItems.appendChild(dragItem);
        });
    }

    handleDragStart(e) {
        const item = e.target.closest('.draggable');
        if (!item) return;

        item.classList.add('dragging');
        e.dataTransfer.effectAllowed = 'move';
        
        // Store item data
        e.dataTransfer.setData('text/plain', item.querySelector('img').src);
        e.dataTransfer.setData('type', item.dataset.type);
        e.dataTransfer.setData('width', item.dataset.width);
        e.dataTransfer.setData('height', item.dataset.height);

        // Show remove zone
        if (this.removeZone) {
            this.removeZone.classList.add('visible');
        }

        // Show grid overlay in drop zones
        this.dropZones.forEach(zone => {
            if (zone !== this.availableItems) {
                const overlay = zone.querySelector('.grid-overlay');
                if (overlay) {
                    overlay.classList.add('visible');
                }
            }
        });
    }

    handleDragEnd(e) {
        const item = e.target.closest('.draggable');
        if (item) {
            item.classList.remove('dragging');
        }

        // Hide remove zone
        if (this.removeZone) {
            this.removeZone.classList.remove('visible');
        }

        // Hide grid overlays
        this.dropZones.forEach(zone => {
            if (zone !== this.availableItems) {
                const overlay = zone.querySelector('.grid-overlay');
                if (overlay) {
                    overlay.classList.remove('visible');
                }
            }
        });
    }

    handleDragOver(e) {
        e.preventDefault();
        e.dataTransfer.dropEffect = 'move';
    }

    handleDragEnter(e) {
        e.preventDefault();
        const dropZone = e.target.closest('.drop-zone');
        if (dropZone) {
            dropZone.classList.add('drag-over');
        }
    }

    handleDragLeave(e) {
        const dropZone = e.target.closest('.drop-zone');
        if (dropZone) {
            dropZone.classList.remove('drag-over');
        }
    }

    handleDrop(e) {
        e.preventDefault();
        const dropZone = e.target.closest('.drop-zone');
        if (!dropZone || dropZone === this.availableItems) return;

        dropZone.classList.remove('drag-over');

        // Get the dragged item
        const draggedItem = document.querySelector('.dragging');
        if (!draggedItem) return;

        // Get drop position
        const { row, col } = this.getGridPosition(e, dropZone);
        
        // Get item dimensions
        const width = parseInt(draggedItem.dataset.width);
        const height = parseInt(draggedItem.dataset.height);

        // Check if the drop position is within bounds
        if (!this.isWithinBounds(dropZone, row, col, width, height)) {
            return;
        }

        // Get only the draggable items from the dropzone
        const existingItems = Array.from(dropZone.querySelectorAll('.draggable:not(.dragging):not(.grid-overlay)'));

        // Skip conflict check if dropzone is empty
        if (existingItems.length === 0) {
            this.finalizeItemPlacement(draggedItem, dropZone, row, col, width, height);
            return;
        }

        // Initialize grid
        const grid = Array(6).fill(null).map(() => Array(6).fill(false));

        // Mark occupied cells in the grid
        existingItems.forEach(item => {
            const itemRow = parseInt(item.dataset.row);
            const itemCol = parseInt(item.dataset.col);
            const itemWidth = parseInt(item.dataset.width);
            const itemHeight = parseInt(item.dataset.height);

            // Skip if any data is invalid
            if (isNaN(itemRow) || isNaN(itemCol) || isNaN(itemWidth) || isNaN(itemHeight)) {
                return;
            }

            // Mark cells as occupied
            for (let r = itemRow; r < itemRow + itemHeight; r++) {
                for (let c = itemCol; c < itemCol + itemWidth; c++) {
                    if (r >= 0 && r < 6 && c >= 0 && c < 6) {
                        grid[r][c] = true;
                    }
                }
            }
        });

        // Check if new item position conflicts with any occupied cells
        let hasConflict = false;
        const conflicts = [];

        // Check each cell that would be occupied by the new item
        for (let r = row; r < row + height && !hasConflict; r++) {
            for (let c = col; c < col + width && !hasConflict; c++) {
                if (r >= 0 && r < 6 && c >= 0 && c < 6 && grid[r][c]) {
                    hasConflict = true;
                    // Find which items are causing the conflict
                    existingItems.forEach(item => {
                        const itemRow = parseInt(item.dataset.row);
                        const itemCol = parseInt(item.dataset.col);
                        const itemWidth = parseInt(item.dataset.width);
                        const itemHeight = parseInt(item.dataset.height);

                        if (!isNaN(itemRow) && !isNaN(itemCol) && !isNaN(itemWidth) && !isNaN(itemHeight)) {
                            if (r >= itemRow && r < itemRow + itemHeight && 
                                c >= itemCol && c < itemCol + itemWidth) {
                                if (!conflicts.includes(item)) {
                                    conflicts.push(item);
                                }
                            }
                        }
                    });
                }
            }
        }

        if (hasConflict) {
            const conflictingNames = conflicts.map(item => {
                const size = `${item.dataset.width}x${item.dataset.height}`;
                return `${size} item at position (${item.dataset.row},${item.dataset.col})`;
            }).join(', ');
            this.showConflictModal(draggedItem, dropZone, row, col, width, height, conflictingNames);
        } else {
            this.finalizeItemPlacement(draggedItem, dropZone, row, col, width, height);
        }
    }

    finalizeItemPlacement(item, dropZone, row, col, width, height) {
        // Create a new item if coming from available items
        const newItem = item.parentElement === this.availableItems ? 
            this.createDraggableItem(
                item.querySelector('img').src,
                item.dataset.type,
                width,
                height
            ) : item;

        // Position the item
        const left = col * 80;
        const top = row * 80;

        newItem.style.position = 'absolute';
        newItem.style.left = `${left}px`;
        newItem.style.top = `${top}px`;
        newItem.style.width = `${width * 80 - 10}px`;
        newItem.style.height = `${height * 80 - 10}px`;
        newItem.style.margin = '5px';

        // Store the position in dataset
        newItem.dataset.row = row.toString();
        newItem.dataset.col = col.toString();
        newItem.dataset.width = width.toString();
        newItem.dataset.height = height.toString();

        // Add to drop zone
        dropZone.appendChild(newItem);
        
        // Clear dragging state
        newItem.classList.remove('dragging');
    }

    getGridPosition(e, dropZone) {
        const rect = dropZone.getBoundingClientRect();
        const padding = 10; // Account for the dropZone padding
        const x = e.clientX - rect.left - padding;
        const y = e.clientY - rect.top - padding;

        // Calculate grid position
        const col = Math.max(0, Math.min(Math.floor(x / 80), 5));
        const row = Math.max(0, Math.min(Math.floor(y / 80), 5));

        return { row, col };
    }

    isWithinBounds(dropZone, row, col, width, height) {
        return row >= 0 && col >= 0 && row + height <= 6 && col + width <= 6;
    }

    findConflicts(dropZone, row, col, width, height, draggedItem) {
        const conflicts = [];
        const gridItems = Array.from(dropZone.querySelectorAll('.draggable'));

        // For each existing item in the drop zone
        for (const item of gridItems) {
            // Skip if it's the same item being dragged
            if (item === draggedItem) continue;

            const itemRow = parseInt(item.dataset.row);
            const itemCol = parseInt(item.dataset.col);
            const itemWidth = parseInt(item.dataset.width);
            const itemHeight = parseInt(item.dataset.height);

            // Skip if any position data is missing
            if (isNaN(itemRow) || isNaN(itemCol) || isNaN(itemWidth) || isNaN(itemHeight)) {
                continue;
            }

            // Check for overlap
            const hasOverlap = (
                row < itemRow + itemHeight &&
                row + height > itemRow &&
                col < itemCol + itemWidth &&
                col + width > itemCol
            );

            if (hasOverlap) {
                conflicts.push(item);
            }
        }

        return conflicts;
    }

    checkOverlap(
        row1, col1, width1, height1,
        row2, col2, width2, height2
    ) {
        // Check if one rectangle is completely to the left of the other
        if (col1 + width1 <= col2 || col2 + width2 <= col1) {
            return false;
        }

        // Check if one rectangle is completely above the other
        if (row1 + height1 <= row2 || row2 + height2 <= row1) {
            return false;
        }

        // If we get here, the rectangles overlap
        return true;
    }

    showConflictModal(draggedItem, dropZone, row, col, width, height, conflictingNames) {
        const modal = document.getElementById('conflict-modal');
        const message = document.getElementById('conflict-message');
        
        if (!modal || !message) return;

        message.textContent = `There is a conflict with: ${conflictingNames}. Would you like to replace the existing item(s), keep both, or cancel?`;
        modal.style.display = 'block';

        // Store current drop information for resolution
        this.pendingDrop = { draggedItem, dropZone, row, col, width, height };

        // Set up resolution handlers if not already set
        if (!this.resolutionHandlersSet) {
            document.getElementById('resolve-replace').addEventListener('click', () => {
                this.resolveConflict('replace');
            });

            document.getElementById('resolve-keep').addEventListener('click', () => {
                this.resolveConflict('keep');
            });

            document.getElementById('resolve-cancel').addEventListener('click', () => {
                this.resolveConflict('cancel');
            });

            this.resolutionHandlersSet = true;
        }
    }

    resolveConflict(action) {
        const modal = document.getElementById('conflict-modal');
        if (!modal || !this.pendingDrop) return;

        const { draggedItem, dropZone, row, col, width, height } = this.pendingDrop;

        switch (action) {
            case 'replace':
                // Remove conflicting items
                const conflicts = this.findConflicts(dropZone, row, col, width, height, draggedItem);
                conflicts.forEach(item => {
                    this.updateGridState(dropZone, item);
                    item.remove();
                });
                // Place the new item
                this.finalizeItemPlacement(draggedItem, dropZone, row, col, width, height);
                break;

            case 'keep':
                // Find the nearest free space
                const freeSpace = this.findNearestFreeSpace(dropZone, width, height);
                if (freeSpace) {
                    this.finalizeItemPlacement(draggedItem, dropZone, freeSpace.row, freeSpace.col, width, height);
                }
                break;

            case 'cancel':
                // Do nothing, just close the modal
                break;
        }

        // Clear pending drop and close modal
        this.pendingDrop = null;
        modal.style.display = 'none';
    }

    findNearestFreeSpace(dropZone, width, height) {
        const grid = this.gridState.get(dropZone.id);
        if (!grid) return null;

        const rows = grid.length;
        const cols = grid[0].length;

        // Try each position in a spiral pattern from the center
        const centerRow = Math.floor(rows / 2);
        const centerCol = Math.floor(cols / 2);
        const maxDistance = Math.max(rows, cols);

        for (let distance = 0; distance < maxDistance; distance++) {
            // Try positions in a square pattern at this distance from center
            for (let i = -distance; i <= distance; i++) {
                for (let j = -distance; j <= distance; j++) {
                    const row = centerRow + i;
                    const col = centerCol + j;

                    if (this.isValidPosition(dropZone, row, col, width, height)) {
                        return { row, col };
                    }
                }
            }
        }

        return null;
    }

    updateGridState(dropZone, item, newRow = null, newCol = null, newWidth = null, newHeight = null) {
        if (!dropZone || dropZone === this.availableItems) return;

        const grid = this.gridState.get(dropZone.id);
        if (!grid) return;

        // Clear old position
        for (let i = 0; i < grid.length; i++) {
            for (let j = 0; j < grid[i].length; j++) {
                if (grid[i][j] === item) {
                    grid[i][j] = null;
                }
            }
        }

        // Set new position if provided
        if (newRow !== null && newCol !== null && newWidth !== null && newHeight !== null) {
            for (let i = newRow; i < newRow + newHeight && i < grid.length; i++) {
                for (let j = newCol; j < newCol + newWidth && j < grid[i].length; j++) {
                    grid[i][j] = item;
                }
            }
        }

        console.log('Grid state updated for', dropZone.id);
    }

    isValidPosition(dropZone, row, col, width, height) {
        if (!dropZone || dropZone === this.availableItems) return false;

        const grid = this.gridState.get(dropZone.id);
        if (!grid) return false;

        const cols = parseInt(dropZone.dataset.cols) || 6;
        const rows = parseInt(dropZone.dataset.rows) || 6;

        // Check bounds
        if (row < 0 || col < 0 || row + height > rows || col + width > cols) {
            return false;
        }

        // Get currently dragged item for self-collision check
        const draggingItem = document.querySelector('.dragging');

        // Check if space is occupied
        for (let i = row; i < row + height; i++) {
            for (let j = col; j < col + width; j++) {
                if (grid[i][j] !== null) {
                    // Skip if it's the same item being dragged
                    if (draggingItem && grid[i][j] === draggingItem) {
                        continue;
                    }
                    return false;
                }
            }
        }

        return true;
    }

    createDraggableItem(imgSrc, type, width, height) {
        const item = document.createElement('div');
        item.classList.add('draggable');
        item.setAttribute('draggable', 'true');
        item.dataset.type = type;
        item.dataset.width = width;
        item.dataset.height = height;

        // Create and add the image
        const img = document.createElement('img');
        img.src = imgSrc;
        img.alt = `${width}x${height} Image`;
        item.appendChild(img);

        // Add size label
        const sizeLabel = document.createElement('span');
        sizeLabel.classList.add('size-label');
        sizeLabel.textContent = `${width}x${height}`;
        item.appendChild(sizeLabel);

        // Set initial size
        if (this.availableItems.contains(item)) {
            item.style.width = `${width * 80 - 10}px`;
            item.style.height = `${height * 80 - 10}px`;
            item.style.margin = '5px';
        }

        // Add drag event listeners
        item.addEventListener('dragstart', this.handleDragStart.bind(this));
        item.addEventListener('dragend', this.handleDragEnd.bind(this));

        return item;
    }

    handleRemoveDrop(e) {
        e.preventDefault();
        this.removeZone.classList.remove('drag-over');
        
        const draggedItem = document.querySelector('.dragging');
        if (draggedItem && draggedItem.parentElement !== this.availableItems) {
            // Clear the item from the grid before removing it
            this.updateGridState(draggedItem.parentElement, draggedItem);
            
            draggedItem.style.transform = 'scale(0)';
            draggedItem.style.opacity = '0';
            setTimeout(() => {
                draggedItem.remove();
            }, 300);
        }
    }
}

// Initialize the drag and drop manager when the DOM is fully loaded
document.addEventListener('DOMContentLoaded', () => {
    new DragDropManager();
});
