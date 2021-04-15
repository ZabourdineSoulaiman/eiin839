//b5311bdd1466122d65678117f6ba1bbbda257fbb

var stations = [];

function retrieveAllContracts(){
    var apiKey = document.getElementById("input").value;
    console.log("Retrieve apiKey : " + apiKey);
    var url = "https://api.jcdecaux.com/vls/v1/contracts?&apiKey=" + apiKey;
    console.log("Url : " + url);
    var request = new XMLHttpRequest();
    request.open("Get", url, true);
    request.setRequestHeader("Accept", "application/json")
    request.onload = contractsRetrieved;
    request.send();
}

function contractsRetrieved(){
    var response = JSON.parse(this.responseText);
    response.forEach(contract => {
        var div = document.createElement("OPTION");
        div.setAttribute("value", contract.name);
        div.setAttribute("id", contract.name);
        document.getElementById("dataConctractList").appendChild(div);
    })
}

function retrieveContractStations(){
    var apiKey = document.getElementById("input").value;
    var contract = document.getElementById("conctractList").value;
    console.log("Retrieve apiKey : " + apiKey + ", contract retrieved : " + contract);
    var url = "https://api.jcdecaux.com/vls/v1/stations?contract="+ contract +"&apiKey=" + apiKey;
    console.log("Url : " + url);
    var request = new XMLHttpRequest();
    request.open("Get", url, true);
    request.setRequestHeader("Accept", "application/json")
    request.onload = stationsRetrieved;
    request.send();
}

function stationsRetrieved(){
    var response = JSON.parse(this.responseText);
    response.forEach(station => {
        stations.push(station);
    })
}

function getClosestStation(){
    var positionLatitude = document.getElementById("positionLatitude").value;
    var positionLongitude = document.getElementById("positionLongitude").value;
    console.log("Retrieve positionLatitude : " + positionLatitude + ", positionLongitude : " + positionLongitude);
    var closestStation = null;
    var distance = null;
    stations.forEach(station=>{
        if(closestStation == null){
            closestStation = station;
            distance = getDistanceFrom2GpsCoordinates(positionLatitude, positionLongitude, station.position.lat, station.position.lng);
        }
        else{
            if(distance >= getDistanceFrom2GpsCoordinates(positionLatitude, positionLongitude, station.position.lat, station.position.lng)){
                closestStation = station;
                distance = getDistanceFrom2GpsCoordinates(positionLatitude, positionLongitude, station.position.lat, station.position.lng);
            }
        }
    })
    console.log(closestStation);
}


function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2-lat1);
    var dLon = deg2rad(lon2-lon1);
    var a =
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon/2) * Math.sin(dLon/2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI/180)
}





















