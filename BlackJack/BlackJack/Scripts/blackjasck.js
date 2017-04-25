$(function () {
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

    hub.client.printCard = function (card) {
        var enc = $('<div />').text(card).html();
        alert(enc);
    };

    $.connection.hub.start().done(function () {
        Console.log("finito");
        hub.server.setNickname($('#name').val());
        Console.log("Nick settato");
        hub.server.newGame();
        Console.log("Nuova partita");
    });
});