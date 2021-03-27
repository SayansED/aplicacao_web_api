var tbody = document.querySelector('table tbody');
var aluno = {};

function Cadastrar() {
    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.ra = document.querySelector('#ra').value;

    /*
    //console.log(aluno)
    var xhr = new XMLHttpRequest();
    // configuração antes de enviar send
    xhr.open('GET', 'https://localhost:44312/api/aluno', true);
    // quando estiver pronto me retorna um texto
    xhr.onload = function () {
        console.log(this.responseText);
    }
    xhr.send();
    */

    console.log(aluno)

    if (aluno.id === undefined || aluno.id === 0) {
        salvarEstudantes('POST', 0, aluno);
    }
    else {
        salvarEstudantes('PUT', aluno.id, aluno);
    }
    carregaEstudantes();
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar')
    var titulo = document.querySelector('#titulo')

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    aluno = {}

    btnSalvar.textContent = 'Cadastrar';
    btnCancelar.textContent = 'Limpar';

    titulo.textContent = 'Cadastrar Aluno'
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
    var btnCancelar = document.querySelector('#btnCancelar')
    var titulo = document.querySelector('#titulo')

    document.querySelector('#nome').value = estudante.nome;
    estudante.sobrenome = document.querySelector('#sobrenome').value = estudante.sobrenome;
    estudante.telefone = document.querySelector('#telefone').value = estudante.telefone;
    estudante.ra = document.querySelector('#ra').value = estudante.ra;

    btnSalvar.textContent = 'Salvar';
    btnCancelar.textContent = 'Cancelar';

    titulo.textContent = `Editar Aluno ${estudante.nome}`;

    aluno = estudante;

    console.log(aluno)
}

function deletarEstudantes(id) {
    var xhr = new XMLHttpRequest();

    xhr.open(`DELETE`, `https://localhost:44312/api/aluno/${id}`, false);
    xhr.send();
}

function deletar(id) {
    deletarEstudantes(id);
    carregaEstudantes();
}

function adicionaLinha(estudantes) {

    var trow = `
                <tr>
                    <td>${estudantes.nome}</td>
                    <td>${estudantes.sobrenome}</td>
                    <td>${estudantes.telefone}</td>
                    <td>${estudantes.ra}</td>
                    <td>
                        <button onclick='editarEstudantes(${JSON.stringify(estudantes)})'>Editar</button>
                        <button onclick='deletar(${estudantes.id})'>Deletar</button>
                    </td>
                </tr>
                `
    //console.log(trow);
    tbody.innerHTML += trow;
}