﻿$(function () {
    // dichiaro la referenza al proxy hub
    var hub = $.connection.gameHub;

    $('#name').val(prompt('Inserisci il tuo nick:', 'nome'));

    //creo la funzione newNickName
    hub.client.newNickName = function (name) {
        //evito injection
        var encName = $('<div />').text(name).html();
        $('#listName').append('<li><strong>' + encName +
                    '</strong>');
    };

    hub.client.printCard = function (name) {
        alert(name);
    };

    $.connection.hub.start().done(function () {
        hub.server.setNickname($('#name').val());

        $('#send').click(function () {
            $('#send').remove();
        });
    });

});