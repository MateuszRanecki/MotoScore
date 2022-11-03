"use strict";
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/tournament-hub')
    .build()

connection.start();