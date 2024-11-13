// Permitir que elementos sejam arrastados
function allowDrop(ev) {
    ev.preventDefault();
}

// Definir o elemento que está sendo arrastado
function drag(ev) {
    ev.dataTransfer.setData("text/plain", ev.target.id);
}

// Criar um novo componente e adicioná-lo à coluna correta
function drop(ev) {
    ev.preventDefault();

    // Verificar qual é o item sendo arrastado
    var data = ev.dataTransfer.getData("text/plain");
    var draggedElement = document.getElementById(data);

    // Verificar o ponto exato onde o item foi solto dentro da coluna
    var column = ev.target.closest('.column');
    if (!column) return;

    // Se o elemento arrastado veio do menu lateral, clonar e adicionar
    if (draggedElement.classList.contains('component')) {
        const task = createTaskElement(draggedElement.innerText);
        
        // Calcular onde soltar na coluna, com base na posição do mouse
        var referenceElement = getClosestTask(column, ev.clientY);
        if (referenceElement) {
            column.insertBefore(task, referenceElement);
        } else {
            column.appendChild(task);
        }
        
        task.querySelector('input[type="text"]').focus();  // Focar no campo de texto para edição
    } else {
        // Se o elemento arrastado já existe na coluna, apenas realocar
        var referenceElement = getClosestTask(column, ev.clientY);
        if (referenceElement) {
            column.insertBefore(draggedElement, referenceElement);
        } else {
            column.appendChild(draggedElement);  // Caso não tenha referência, adiciona ao final da coluna
        }
    }
}

// Função para criar um novo componente com checkbox e campo de texto editável
function createTaskElement(text) {
    const task = document.createElement('div');
    task.classList.add('task');
    task.setAttribute('draggable', 'true');
    task.addEventListener('dragstart', drag);

    // Criar o conteúdo da tarefa com checkbox e campo de input editável
    task.innerHTML = `
        <input type="checkbox" onchange="toggleTask(this)">
        <input type="text" value="${text}" onfocus="enableEdit(this)" onblur="disableEdit(this)">
    `;

    return task;
}

// Função para identificar o elemento mais próximo do ponto de drop (soltar)
function getClosestTask(column, mouseY) {
    const tasks = [...column.querySelectorAll('.task')];

    return tasks.reduce((closest, child) => {
        const box = child.getBoundingClientRect();
        const offset = mouseY - box.top - box.height / 2;

        if (offset < 0 && offset > closest.offset) {
            return { offset: offset, element: child };
        } else {
            return closest;
        }
    }, { offset: Number.NEGATIVE_INFINITY }).element;
}

// Função para riscar as tarefas quando completadas
function toggleTask(checkbox) {
    const task = checkbox.parentElement;
    if (checkbox.checked) {
        task.classList.add('completed');
    } else {
        task.classList.remove('completed');
    }
}

// Funções para tornar o campo de texto editável e desabilitar edição
function enableEdit(input) {
    input.style.cursor = 'text';  // Alterar cursor para modo de texto durante edição
    input.readOnly = false;       // Habilitar edição
}

function disableEdit(input) {
    input.style.cursor = 'pointer';  // Retornar cursor normal
    input.readOnly = true;           // Desabilitar edição quando o foco for perdido
}

// Adicionar eventos de arrastar nos componentes da barra lateral
document.querySelectorAll('.component').forEach(component => {
    component.addEventListener('dragstart', drag);
});
