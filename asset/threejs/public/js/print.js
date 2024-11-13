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

            // 'cname'이 'projectnum'인 값을 찾아 projectNum 변수에 저장
            if (item.cname === 'projectnum') {
              projectNum = el.val;
            }         
          $("." + item.cname)
            .eq(el.idx)
            .html(el.val); 
        } 

        let projectNumValue = o.items.find(item => item.cname === "projectnum");
        let zoneNumValue = o.items.find(item => item.cname === "zonenum");
        let coolingNumValue = o.items.find(item => item.cname === "coolingnum");
        $(".buildingImage").each((idx,al) => {
          let projectNum = projectNumValue.data[idx].val; 
          al.setAttribute("src", "img/" + projectNum + "/Building.png");
        });
        $(".zoneImage").each((idx,al) => {
          let projectNum = projectNumValue.data[idx].val; 
          let zoneNum = zoneNumValue.data[idx].val; 
          al.setAttribute("src", "img/" + projectNum + "/"+ zoneNum + ".png"); 
        });
        $(".coolingImage").each((idx,al) => {
          let projectNum = projectNumValue.data[idx].val; 
          let coolingNum = coolingNumValue.data[idx].val; 
          al.setAttribute("src", "img/" + projectNum + "/"+ coolingNum + ".png"); 
        });
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
