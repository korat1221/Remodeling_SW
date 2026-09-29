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
        let zoneLayersValue = o.items.find(item => item.cname === "zone_layers");
        let coolingNumValue = o.items.find(item => item.cname === "coolingnum");
        $(".buildingImage").each((idx,al) => {
          let projectNum = projectNumValue.data[idx].val; 
          al.setAttribute("src", "img/" + projectNum + "/Building.png");
        });
        $(".zoneImage").each((idx,al) => {
          if (!zoneLayersValue || !zoneLayersValue.data[idx]) return;

          al.innerHTML = "";
          let zoneScale = Math.min(al.getBoundingClientRect().width / 953, al.getBoundingClientRect().height / 370);
          if (!Number.isFinite(zoneScale) || zoneScale <= 0) zoneScale = 1;
          zoneLayersValue.data[idx].val.forEach((layer) => {
            if (layer.s) {
              let img = document.createElement("img");
              let src = layer.s || "";
              img.setAttribute("src", /^(data:|https?:|file:)/i.test(src) ? src : "../" + src.replace(/^[/\\]+/, ""));
              img.style.position = "absolute";
              img.style.left = (layer.x / 953 * 100) + "%";
              img.style.top = (layer.y / 370 * 100) + "%";
              img.style.width = (layer.w / 953 * 100) + "%";
              img.style.height = (layer.h / 370 * 100) + "%";
              img.style.objectFit = "contain";
              img.style.objectPosition = "center";
              al.appendChild(img);
              return;
            }

            let text = document.createElement("div");
            text.textContent = layer.t || "";
            text.style.position = "absolute";
            text.style.left = (layer.x / 953 * 100) + "%";
            text.style.top = (layer.y / 370 * 100) + "%";
            text.style.width = (layer.w / 953 * 100) + "%";
            text.style.height = (layer.h / 370 * 100) + "%";
            text.style.display = "flex";
            text.style.alignItems = "center";
            text.style.justifyContent = layer.a === "center" ? "center" : "flex-start";
            text.style.boxSizing = "border-box";
            text.style.whiteSpace = "nowrap";
            text.style.overflow = "hidden";
            text.style.fontFamily = layer.box ? "나눔바른고딕, sans-serif" : "굴림, sans-serif";
            text.style.fontSize = (layer.fs * zoneScale) + "pt";
            text.style.fontWeight = layer.b ? "bold" : "normal";
            text.style.color = layer.c || "#000";
            if (layer.box) {
              text.style.backgroundColor = "#fff";
              text.style.border = "1px solid #7f7f7f";
            }
            al.appendChild(text);
          });
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
