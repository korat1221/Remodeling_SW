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
        
       if (item.cname === "zebLevel") {
         item.data.forEach(el => {
          let $target = $(".zebLevel").eq(el.idx);

            switch (el.val) {
              case "ZEB 5등급":
              $target.css("background-color", "#FFC000"); // 연한 빨강
              break;
              case "ZEB 4등급":
                $target.css("background-color", "#FFF2CC"); // 연한 초록
                break;
              case "ZEB 3등급":
                $target.css("background-color", "#C6E0B4"); // 연한 파랑
                break;
              case "ZEB 2등급":
                $target.css("background-color", "#92D050"); // 연한 파랑
                break;
              case "ZEB 1등급":
                $target.css("background-color", "#009900"); // 연한 파랑
                break;
              case "None":
                $target.css("background-color", "red"); // 연한 파랑
                break;
              default:
                $target.css("background-color", "red"); // 기본 회색
                break;
            }       
          });
        }

        let projectNumValue = o.items.find(item => item.cname === "projectnum");
        let zoneNumValue = o.items.find(item => item.cname === "zonenum");
        let coolingNumValue = o.items.find(item => item.cname === "coolingnum");
        let heatingNumValue = o.items.find(item => item.cname === "heatingnum");
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
        $(".heatingImage").each((idx,al) => {
          let projectNum = projectNumValue.data[idx].val; 
          let heatingNum = heatingNumValue.data[idx].val; 
          al.setAttribute("src", "img/" + projectNum + "/"+ heatingNum + ".png"); 
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
