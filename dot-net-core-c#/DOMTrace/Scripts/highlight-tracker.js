let previouslyHighlighted = null;

function highlightElement(el) {
    if (previouslyHighlighted) {
        previouslyHighlighted.style.outline = '';
    }
    previouslyHighlighted = el;
    el.style.outline = '2px solid yellow';
}

function getXPath(element) {
    if (element.id !== '') return 'id(\"' + element.id + '\")';
    if (element === document.body) return element.tagName;

    var ix = 0;
    var siblings = element.parentNode.childNodes;
    for (var i = 0; i < siblings.length; i++) {
        var sibling = siblings[i];
        if (sibling === element) return getXPath(element.parentNode) + '/' + element.tagName + '[' + (ix + 1) + ']';
        if (sibling.nodeType === 1 && sibling.tagName === element.tagName) ix++;
    }
}

document.addEventListener('contextmenu', function (e) {
    e.preventDefault();
    var xpath = getXPath(e.target);
    bound.reportXPath(xpath);
    return false;
}, true);

document.addEventListener('mousemove', function (e) {
    highlightElement(e.target);
}, true);