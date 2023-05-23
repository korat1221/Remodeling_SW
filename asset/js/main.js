
var gMainTree = new MainTree((data) => {
    window.chrome.webview.postMessage(data);
});
