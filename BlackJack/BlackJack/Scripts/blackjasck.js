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

    hub.client.printCardP = function (card) {
        $('#listCardsP').append('<img src="' + card + '" style="height:150px"/>');
    };

    hub.client.printCardD = function (card) {
        $('#listCardsD').append('<img src="' + card + '" style="height:150px"/>');
    };

    hub.client.flushTable = function () {
        $('#listCardsD').empty();
        $('#listCardsP').empty();
        $('#hit').removeAttr('disabled');
        $('#stay').removeAttr('disabled');
        $('#start').attr('disabled', 'disabled');
        $('#perso').attr('hidden', 'hidden');
        $('#vinto').attr('hidden', 'hidden');
    };

    hub.client.printWinner = function (nome) {
        $('#start').removeAttr('disabled');
        $('#hit').attr('disabled', 'disabled');
        $('#stay').attr('disabled', 'disabled');
        if (nome == "Dealer") {
            $('#perso').removeAttr('hidden');
        }
        else {
            $('#vinto').removeAttr('hidden');
        }
        alert(nome + " ha vinto!");
    };

    hub.client.printPoints = function (dealer, player) {
        $('#points').empty();
        $('#points').append('<p>Dealer: ' + dealer + ' Player: ' + player);
    };

    hub.client.updNickName = function () {
        $('#listName').empty();
    }

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