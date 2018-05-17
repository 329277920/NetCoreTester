function run() {
    var uri = 'http://localhost:6666/secret';
          
    console.writeLine(http.get(uri).toString());
  
    return 1;
}