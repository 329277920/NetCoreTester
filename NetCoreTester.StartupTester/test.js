function run() {

    var uri = 'http://192.168.10.75:10000/';

for(var i=0;i<100;i++){
   var doc = http.get(uri).toString();
}      
    
    
    // console.writeLine(doc);

    // 返回成功
    return 1;
}