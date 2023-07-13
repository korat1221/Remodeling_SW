
window.addEventListener("message", async (event) => {
    let o = event.data;
    if (o.print) {
        window.print();
    }
    else if (o.init) {
        let i = -1;

        while(++i < o.zpages.length) {
            let zpage = o.zpages[i];

            $( 'html' ).append($('<div>').load('zpage.html'));
        }
    }
});

$(function(){
    let i = -1;

    while(++i < 4) {
        $( 'body' ).append($('<div>').load('zpage.html'));
    }
});
