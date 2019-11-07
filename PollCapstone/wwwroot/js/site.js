var decoyArray = [];

function addFields() {
    var number = document.getElementById("member").value;
    var container = document.getElementById("container");
    var choiceArray = [];
    while (container.hasChildNodes()) {
        container.removeChild(container.lastChild);
    }
    for (i = 0; i < number; i++) {
        container.appendChild(document.createTextNode((i + 1) + " "));
        var input = document.createElement("input");
        input.type = "text";
        choiceArray.push(input);
        container.appendChild(input);
        container.appendChild(document.createElement("br"));
        container.appendChild(document.createElement("br"));
    }
    decoyArray = choiceArray;
    //var jsonString = JSON.stringify(choiceArray);
    //alert(jsonString);
}

function putFields() {
    var jsonString = JSON.stringify(decoyArray);
    console.log(decoyArray);
}
