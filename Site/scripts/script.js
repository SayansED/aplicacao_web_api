var tbody = document.querySelector('table tbody');
var aluno = {};

function Cadastrar() {
    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.ra = document.querySelector('#ra').value;

    console.log(aluno)

    if (aluno.id === undefined || aluno.id === 0) {
        salvarEstudantes('POST', 0, aluno);
    }
    else {
        salvarEstudantes('PUT', aluno.id, aluno);
    }
    carregaEstudantes();

    $('#myModal').modal('hide');
}

function novoAluno() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal')

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    aluno = {}

    btnSalvar.textContent = 'Cadastrar';

    tituloModal.textContent = 'Cadastrar Aluno'

    $('#myModal').modal('show');
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal')

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    aluno = {}

    btnSalvar.textContent = 'Cadastrar';

    tituloModal.textContent = 'Cadastrar Aluno'

    $('#myModal').modal('hide');
}

function carregaEstudantes() {
    tbody.innerHTML = '';
    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `https://localhost:44312/api/aluno`, true);

    xhr.onload = function () {
        var estudantes = JSON.parse(this.responseText); // estudantes é um arquivo json
        for (var indice in estudantes) {
            adicionaLinha(estudantes[indice]);
        }
    }
    xhr.send();
}

function salvarEstudantes(metodo, id, corpo) {
    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0) {
        id = '';
    }

    xhr.open(metodo, `https://localhost:44312/api/aluno/${id}`, false);

    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));
}

carregaEstudantes();

function editarEstudantes(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal')

    document.querySelector('#nome').value = estudante.nome;
    estudante.sobrenome = document.querySelector('#sobrenome').value = estudante.sobrenome;
    estudante.telefone = document.querySelector('#telefone').value = estudante.telefone;
    estudante.ra = document.querySelector('#ra').value = estudante.ra;

    btnSalvar.textContent = 'Salvar';

    tituloModal.textContent = `Editar Aluno ${estudante.nome}`;

    aluno = estudante;

    console.log(aluno)
}

function deletarEstudantes(id) {
    var xhr = new XMLHttpRequest();

    xhr.open(`DELETE`, `https://localhost:44312/api/aluno/${id}`, false);
    xhr.send();
}

function deletar(estudante) {

    bootbox.confirm({
        message: `Tem certeza que deseja excluir o estudante ${estudante.nome}`,
        buttons: {
            confirm: {
                label: 'SIM',
                className: 'btn-success'
            },
            cancel: {
                label: 'NÃO',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if(result) {
                deletarEstudantes(estudante.id);
                carregaEstudantes();
            }
        }
    });
}

function adicionaLinha(estudantes) {

    var trow = `
                <tr>
                    <td>${estudantes.nome}</td>
                    <td>${estudantes.sobrenome}</td>
                    <td>${estudantes.telefone}</td>
                    <td>${estudantes.ra}</td>
                    <td>
                        <button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='editarEstudantes(${JSON.stringify(estudantes)})'>Editar</button>
                        <button class="btn btn-danger" onclick='deletar(${JSON.stringify(estudantes)})'>Deletar</button>
                    </td>
                </tr>
                `
    //console.log(trow);
    tbody.innerHTML += trow;
}