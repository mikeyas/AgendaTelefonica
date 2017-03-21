var url = window.location.href.toString();
if (url == "http://localhost:49678/Home/Contact") {
    buttonSalvarContato.onclick = function () {

        var nome = textBoxNome.value.trim();
        var telefone = textBoxTelefone.value.trim();
        var lembranca = textBoxLembranca.value.trim();

        if (nome == "" || telefone == "" || lembranca == "") {
            alert("Preencha o campo!");
            return;
        }

        //Enviar para o servidor (C#)

        $.ajax({
            url: "/Servicos/SalvarContato",
            data: { nome: nome, telefone: telefone, lembranca: lembranca },
            method: "POST"
        }).done(function (resultado) {
            if (resultado == true) {
                alert("Contato Salvo!");
            } else {
                alert("ERRO!");
            }
        })

        textBoxNome.value = "";
        textBoxTelefone.value = "";
        textBoxLembranca.value = "";
    }
}

var minhaApp = angular.module("minhaApp", [])
    .controller("ContatosController", function ($scope) {
    $scope.contatos = [];
    $.ajax({
        url: "/Servicos/Contatos",
        method: "GET"
    }).done(function (resultado) {
        $scope.contatos = resultado;
        $scope.$apply();
    });


});

