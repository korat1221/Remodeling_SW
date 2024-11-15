const express = require('express')
const cors = require('cors')
const app = express()
const path = require('path')
let fs = require('fs');

app.use(cors());

app.use(express.static(__dirname))
 
app.use(
  '/build/',
  express.static(path.join(
    __dirname,
    'node_modules/three/build'
  ))
)
app.use(
  '/jsm/',
  express.static(path.join(
    __dirname,
    'node_modules/three/examples/jsm'
  ))
)

app.use((req, res, next) => {
  req.setEncoding('utf8');
  req.rawBody = '';
  req.on('data', function(chunk) {
    req.rawBody += chunk;
  });
  req.on('end', function(){
    next();
  });
});

app.post('/upload', (req, res) => {
  let params = req.rawBody;
  params = params.split("&");
  let _getParam = (sname) => {
    var sval = "";

    for (var i = 0; i < params.length; i++) {
        temp = params[i].split("=");
        if ([temp[0]] == sname) { sval = temp[1]; }
    }
    return sval;
  }

  let path0 = __dirname + "/models/" + _getParam("pid");
  let path = path0 + "/model.json";

  fs.mkdirSync(path0, { recursive: true });

  fs.writeFile(path,_getParam("json"),function(err){
    if (err === null) {
        res.json({"res":"success"});
    } else {
        res.json({"res":"fail"});
    }
  });
});

app.listen(
  3000,
  () => console.log('http://localhost:3000')
) 