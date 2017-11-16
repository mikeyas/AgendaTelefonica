var minhaApp = angular.module("minhaApp", [])
    .controller("UsuariosController", function ($scope) {
        $scope.usuarios = [];
        $.ajax({
            url: "/Autenticacao/GerenciarUsuarios",
            method: "GET"
        }).done(function (resultado) {
            $scope.usuarios = resultado;
            $scope.$apply();
        });

        $scope.edit = function (id) {
            var i = 0;
            var a = false;
            while (i < $scope.usuarios.length && a === false) {
                if ($scope.usuarios[i].Id === id) {
                    a = true;
                    alert("entrou while/if");
                } else {
                    i++;
                    alert("entrou while/else " + i + " " + $scope.usuarios[i].Id);
                }
            }
            if (i < $scope.usuarios.length) {
                window.sessionStorage.setItem('usuario', JSON.stringify($scope.contatos[i]));
                window.location.href = 'Autenticacao/EditarUsuario';
                alert("entrou if");
            } else {
                alert('Item não encontrado');
            }
        };

        $scope.sendedit = function (usuario) {

            var nome = textBoxNome.value.trim();
            var email = textBoxEmail.value.trim();
            var id = $scope.contato.Id;
            alert("entrou" + nome + id);

            if (nome === "" || email === "") {
                alert("Preencha o campo!");
                return;
            }


            //Enviar para o servidor (C#)

            $.ajax({
                url: "/Servicos/AddContato",
                data: { id: id, nome: nome, telefone: telefone, lembranca: lembranca },
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
                    data: { id: id },
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
