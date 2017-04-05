$(function () {
    // dichiaro la referenza al proxy hub
    var hub = $.connection.chatHub;

    // creo la function broadcast
    hub.client.broadcast = function (name, message) {
        // evito injection
        var encName = $('<div />').text(name).html();
        var encMessage = $('<div />').text(message).html();
        $('#forum').append('<li><strong>' + encName +
                    '</strong>:&nbsp;' + encMessage + '</li>');
    }
    $('#name').val(prompt('Inserisci il tuo nick:', 'nome'));

    //creo la funzione newNickName
    hub.client.newNickName = function (name) {
        //evito injection
        var encName = $('<div />').text(name).html();
        $('#listName').append('<li><strong>' + encName +
                    '</strong>');
    };

    $.connection.hub.start().done(function () {
        hub.server.setNickname($('#name').val());
    });
});