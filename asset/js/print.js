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
        let zoneLayersValue = o.items.find(item => item.cname === "zone_layers");
        let coolingNumValue = o.items.find(item => item.cname === "coolingnum");
        let heatingLayersValue = o.items.find(item => item.cname === "heating_layers");
        let dhwNumValue = o.items.find(item => item.cname === "dhwnum");
        let lightingNumValue = o.items.find(item => item.cname === "lightingnum");
        let lightingH = o.items.find(item => item.cname === "lightingHeightnum");
        let lightingT = o.items.find(item => item.cname === "lightingType");
        let ahuLayersValue = o.items.find(item => item.cname === "ahu_layers");

        $(".buildingImage").each((idx,al) => {          
          if (projectNumValue.data[idx]) {
          let projectNum = projectNumValue.data[idx].val; 
          al.setAttribute("src", "../projects/" + projectNum + "/Building.png");
          }
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
          if (projectNumValue.data[idx]) {
          let projectNum = projectNumValue.data[idx].val; 
          let coolingNum = coolingNumValue.data[idx].val; 
          if(coolingNum !== null)
          {
            al.setAttribute("src", "../projects/" + projectNum + "/"+ coolingNum + ".png"); 
          }
          }
        });
        $(".heatingImage").each((idx,al) => {
          if (!heatingLayersValue || !heatingLayersValue.data[idx]) return;

          al.innerHTML = "";
          heatingLayersValue.data[idx].val.forEach((layer) => {
            if (!layer.s) return;

            let img = document.createElement("img");
            let src = layer.s || "";
            img.setAttribute("src", /^(data:|https?:|file:)/i.test(src) ? src : "../" + src.replace(/^[/\\]+/, ""));
            img.style.position = "absolute";
            img.style.left = (layer.x / 900 * 100) + "%";
            img.style.top = (layer.y / 290 * 100) + "%";
            img.style.width = (layer.w / 900 * 100) + "%";
            img.style.height = (layer.h / 290 * 100) + "%";
            img.style.objectFit = "contain";
            img.style.objectPosition = "center";
            al.appendChild(img);
          });
        });
        $(".dhwImage").each((idx,al) => {
          if (projectNumValue.data[idx]) {
          let projectNum = projectNumValue.data[idx].val; 
          let dhwNum = dhwNumValue.data[idx].val; 
          if(dhwNum !== null)
             {
              al.setAttribute("src", "../projects/" + projectNum + "/"+ dhwNum + ".png"); 
             }
          }
        });
          $(".lightingImage").each((idx, al) => {
            if (projectNumValue.data[idx]) {
              let projectNum = projectNumValue.data[idx].val; 
              let lightingNum = lightingNumValue.data[idx].val; 
             if(lightingNum !== null)
             {
              al.setAttribute("src", "../projects/" + projectNum + "/"+ lightingNum + ".png"); 
             }
            }
        });
          $(".lightingHeightImage").each((idx,al) => {
            if (lightingH.data[idx]) {
              let lightingHeight = lightingH.data[idx].val; 
              if (lightingHeight !== null) {
              al.setAttribute("src", "image/light/" + lightingHeight + ".png"); 
              }
            }
        });
          $(".lightingType").each((idx,al) => {
            if (lightingT.data[idx]) {
              let lightingType = lightingT.data[idx].val;
              if (lightingType !== null) {
                al.setAttribute("src", "image/light/" + lightingType + ".png");
              }
            }
        });
          $(".ahuImage").each((idx,al) => {
            if (!ahuLayersValue || !ahuLayersValue.data[idx]) return;

            al.innerHTML = "";
            ahuLayersValue.data[idx].val.forEach((layer) => {
              let img = document.createElement("img");
              let src = layer.s || "";
              img.setAttribute("src", /^(data:|https?:|file:)/i.test(src) ? src : "../" + src.replace(/^[/\\]+/, ""));
              img.style.position = "absolute";
              img.style.left = (layer.x / 990 * 100) + "%";
              img.style.top = (layer.y / 365 * 100) + "%";
              img.style.width = (layer.w / 990 * 100) + "%";
              img.style.height = (layer.h / 365 * 100) + "%";
              img.style.objectFit = "contain";
              img.style.objectPosition = "center";
              al.appendChild(img);
            });
        });  
        
      }
 
      setTimeout(() => {
        const ifrms = document.querySelectorAll('.ifrm-chart1');

        i = -1;
          while (++i < ifrms.length) {
              if (o.chart[i]) {
                  o.chart[i].chart = true;
                  ifrms[i].contentWindow.postMessage(o.chart[i], "*");
              }
        }
      }, 1000);
    }, 100);
  }
});

$(function () {});
