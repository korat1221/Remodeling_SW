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

  let pid = _getParam("pid");
  let path = __dirname + "/projects/" + pid + ".json";
  let path2 = __dirname + "/projects/" + pid + ".obj";

  fs.writeFile(path,Buffer.from(_getParam("json"),'base64').toString('utf8'),function(err){
    if (err === null) {
      fs.access(path2, fs.constants.F_OK, (err) => { // A
        if (err) return console.log('access denied.');
      
        fs.unlink(path2, (err) => err ?  
          console.log(err) : console.log(`${path2} deleted.`));
      });
      fs.writeFile(__dirname + "/projects/execute.sql",Buffer.from(_getParam("sql"),'base64').toString('utf8'),function(err){
        if (err === null) {
          fs.writeFile(__dirname + "/projects/" + pid + ".tree",Buffer.from(_getParam("tree"),'base64').toString('utf8'),function(err){
            if (err === null) {
                res.json({"res":"success"});
            } else {
                res.json({"res":"fail"});
            }
          });
        } else {
            res.json({"res":"fail"});
        }
      });
    } else {
    res.json({"res":"fail"});
  }
  });
});

app.listen(
  3000,
  () => console.log('http://localhost:3000')
) 