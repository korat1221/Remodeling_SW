window.addEventListener("message", async (event) => {
  let o = event.data;
  if (o.print) {
    window.print();
  } else if (o.init) {
    let i = -1,
      j;

    $("div").remove();

    while (++i < o.pages.length) {
      if (i > 0) {
        $("body").append("<div style='page-break-before:always'></div>");
      }
      $("body").append($("<div>").load("/print/" + o.pages[i]));
    }

    setTimeout(() => {
      let i = -1;

      while (++i < o.items.length) {
        let item = o.items[i];

        j = -1;
        while (++j < item.data.length) {
          let el = item.data[j];
          $("." + item.cname)
            .eq(el.idx)
            .html(el.val);
        }
      }
   
        let projectNumValue = o.items.find(item => item.cname === "projectnum");
        console.log("projectNumValue : ",projectNumValue );
        if (projectNumValue) {
            let projectNum = projectNumValue.data[0].val; 
            $("#buildingImage").attr("src", "img/" + projectNum + "/Building.png"); 
        }

        let zoneNumValue = o.items.find(item => item.cname === "zonenum");
        console.log("zoneNumValue : ",zoneNumValue );
        if (zoneNumValue) {
            let projectNum = projectNumValue.data[0].val; 
            let zoneNum = zoneNumValue.data[0].val; 
            $("#zoneImage").attr("src", "img/" + projectNum + "/"+ zoneNum + ".png"); 
        }


      setTimeout(() => {
        const ifrms = document.querySelectorAll('.ifrm-chart1');

        i = -1;
        while(++i < ifrms.length) {
          o.chart[i].chart = true;
          ifrms[i].contentWindow.postMessage(o.chart[i], "*");
        }
      }, 1000);
    }, 100);
  }
});

$(function () {});
