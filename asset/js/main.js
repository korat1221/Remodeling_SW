
let tmClicked = null, command = null;
var gMainTree = new MainTree((data) => {
    let tm = new Date();

    if (!command || command !== data || !tmClicked || (tm - tmClicked) > 1000) {

        window.chrome.webview.postMessage(data);

        command = data;
    }
    tmClicked = tm;
});
