
window.addEventListener("message", async (event) => {
    let o = event.data;
    if (o.print) {
        window.print();
    }
    else if (o.init) {
        let i = -1, j;

        $( "div" ).remove();

        while(++i < o.pages.length) {
            if (i > 0) {
                $( 'body' ).append("<div style='page-break-before:always'></div>");
            }
            $( 'body' ).append($('<div>').load('/print/' + o.pages[i]));
        }
        
        setTimeout(() => {
            let i = -1;

            while(++i < o.items.length) {
                let item = o.items[i];
    
                j = -1;
                while(++j < item.data.length) {
                    let el = item.data[j];
                    $( '.' + item.cname ).eq(el.idx).html(el.val);
                }
            }    
        }, 100);
    }
});

$(function(){
});
