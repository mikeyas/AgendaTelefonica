var url = window.location.href.toString();
if (url === "http://localhost:49678/Home/Contact") {
    buttonSalvarContato.onclick = function () {
        alert("entrou");
        var nome = textBoxNome.value.trim();
        var telefone = textBoxTelefone.value.trim();
        var lembranca = textBoxLembranca.value.trim();

        if (nome === "" || telefone === "" || lembranca === "") {
            alert("Preencha o campo!");
            return;
        }
    

        //Enviar para o servidor (C#)

        $.ajax({
            url: "/Servicos/SalvarContato",
            data: { nome: nome, telefone: telefone, lembranca: lembranca },
            method: "POST"
        }).done(function (resultado) {
            if (resultado === true) {
                alert("Contato Salvo!");
            } else {
                alert("ERRO!");
            }
        });

        textBoxNome.value = "";
        textBoxTelefone.value = "";
        textBoxLembranca.value = "";
        alert("Operação realizada com sucesso!");
        window.location.href = '/';
    };
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

            $scope.edit = function (id) {
                var i = 0;
                var a = false;
                while (i < $scope.contatos.length && a === false) {
                    if ($scope.contatos[i].Id === id) {
                        a = true;
                        alert("entrou while/if");
                    } else {
                        i++;
                        alert("entrou while/else " + i + " " + $scope.contatos[i].Id);
                    }
                }
                if (i < $scope.contatos.length) {
                    window.sessionStorage.setItem('contato', JSON.stringify($scope.contatos[i]));
                    window.location.href = 'Home/EditContact';
                    alert("entrou if");
                } else {
                    alert('Item não encontrado');
                }
            };        

            $scope.sendedit = function (contato) {
                
                var nome = textBoxNome.value.trim();
                var telefone = textBoxTelefone.value.trim();
                var lembranca = textBoxLembranca.value.trim();
                var id = $scope.contato.Id;
                alert("entrou" + nome + id);

                if (nome === "" || telefone === "" || lembranca === "") {
                    alert("Preencha o campo!");
                    return;
                }


                //Enviar para o servidor (C#)

                $.ajax({
                    url: "/Servicos/AddContato",
                    data: { id:id, nome: nome, telefone: telefone, lembranca: lembranca },
                    method: "POST"
                }).done(function (resultado) {
                    if (resultado === true) {
                        alert("Contato Editado!");
                    } else {
                        alert("ERRO!");
                    }
                });

                textBoxNome.value = "";
                textBoxTelefone.value = "";
                textBoxLembranca.value = "";
                alert("Operação realizada com sucesso!");
                window.sessionStorage.removeItem('contato');
                window.location.href = '/';
            };

            $scope.remove = function (contato) {

                var id = contato.Id;
                alert("entrou " + id);

                if (confirm("Deseja realmente excluir contato?")) {
                    
                    $.ajax({
                        url: "/Servicos/RemoveContato",
                        data: {id : id },
                        method: "POST"
                    }).done(function (resultado) {
                        if (resultado === true) {
                            alert("Contato Removido!");
                        } else {
                            alert("ERRO!");
                        }
                    });

                    alert("Operação realizada com sucesso!");
                    window.sessionStorage.removeItem('contato');
                    window.location.href = '/';
                }
            };

            $scope.details = function (id) {
                var i = 0;
                var a = false;
                while (i < $scope.contatos.length && a === false) {
                    if ($scope.contatos[i].Id === id) {
                        a = true;
                        alert("entrou while/if");
                    } else {
                        i++;
                        alert("entrou while/else " + i + " " + $scope.contatos[i].Id);
                    }
                }
                if (i < $scope.contatos.length) {
                    window.sessionStorage.setItem('contato', JSON.stringify($scope.contatos[i]));
                    window.location.href = '/Home/DetailsContact';
                    alert("entrou if");
                } else {
                    alert('Item não encontrado');
                }
            }; 

            $scope.init = function () {
                $scope.contato = JSON.parse(window.sessionStorage.getItem('contato'));
                window.sessionStorage.removeItem('contato');
            };
            $scope.init();
        });

    buttonLogin.onclick = function () {
        var usuario = textBoxUsuario.value.trim();
        var senha = passwordBoxSenha.value.trim();
        var returnUrl = url;

        if (usuario === "" || senha === "") {
            alert("Preencha o campo!");
            return;
        }
        alert(usuario + " " + senha + " " + returnUrl);

        //Enviar para o servidor (C#)
        $.ajax({
            url: "/Autenticacao/LogOn",
            data: { usuario: usuario, senha: senha, returnUrl: returnUrl },
            method: "POST"
        }).done(function (resultado) {
            if (resultado === true) {
                alert("Login Efetuado!");
            } else {
                alert("Usuário ou senha inválido!");
            }
            });
        textBoxUsuario.value = "";
        passwordBoxSenha.value = "";
    };