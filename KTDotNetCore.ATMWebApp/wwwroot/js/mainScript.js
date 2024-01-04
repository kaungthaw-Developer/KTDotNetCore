function getData() {
    var list = [];
    var jsonString = localStorage.getItem('Table_UserInfo');
    console.log(jsonString);
    console.log(JSON.parse(jsonString));
    if (!(jsonString == undefined || jsonString === null || jsonString === '')) {
        list = JSON.parse(jsonString);
    }
    return list;
}