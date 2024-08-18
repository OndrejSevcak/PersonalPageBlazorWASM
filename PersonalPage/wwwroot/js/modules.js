export function enableCodeHighlight() {
    document.querySelectorAll("pre code").forEach((elm) => {
        hljs.highlightBlock(elm);
    })
}

export function appendTextIntoTextArea(elementId, textToAppend) {
    const ta = document.getElementById(elementId);
    if (ta != null) {
        ta.value = ta.value + textToAppend;
    }
}