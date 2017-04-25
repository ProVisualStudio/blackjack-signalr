$(function () {
    // dichiaro la referenza al proxy hub
    var hub = $.connection.gameHub;

    $('#name').val(prompt('Inserisci il tuo nick:', 'nome'));

    //creo la funzione newNickName
    hub.client.newNickName = function (id,name) {
        //evito injection
        var encName = $('<div />').text(name).html();
        $('#listName').append('<li id="'+id+'"><strong>' + encName +
                    '</strong>');
    };

    hub.client.printCard = function (card) {
        $('#listCards').append('<li><strong>' + card +
                    '</strong>');
    };

    hub.client.updNickName = function () {
        $('#listName').empty();
    };

    hub.client.addCardP = function (card) {
        $('#playerCards').append('<li><strong>' + card + '</strong></li>');
    };

    $.connection.hub.start().done(function () {
        hub.server.setNickname($('#name').val());

        $('#start').click(function () {
            $('#listCards').empty();
            hub.server.newGame();
        });
        $('#hit').click(function () {
            hub.server.hit();
        });
        $('#stay').click(function () {
            hub.server.stay();
        });
    });

});