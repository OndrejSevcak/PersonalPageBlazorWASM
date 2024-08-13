export function enableCodeHighlight() {
    document.querySelectorAll("pre code").forEach((elm) => {
        hljs.highlightBlock(elm);
    })
}