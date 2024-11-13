function updateProgressBar() { /* a função vai atualizar a barra de progresso*/
    const today = new Date();
            /* const: nao sera reatribuida. today: vai guardar a data atual.
            new Date pega a data e hora do computador*/

            /* today.getFullYear pega o ano atual e o getmonth o mes atual*/
            const endOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0); // Último dia do mês

            /*endMonth retorna o dia do mes atual*/
            const totalDaysInMonth = endOfMonth.getDate();

            /*totalDaysInmonth - daysleft da o numero de dias que ja se foi do mes, divide pelo total de dias
            para ter em fração o total de dias que se passarma. depois multiplica por 100*/
            const daysLeft = totalDaysInMonth - today.getDate();

            /*Math.floor arrendonda o numero pra menos para que passe um numero inteiro*/
            const progressPercentage = Math.floor(((totalDaysInMonth - daysLeft) / totalDaysInMonth) * 100);


            /*atualização da barra de progresso no html */
            const progressBar = document.getElementById('progressBar');
            progressBar.style.width = progressPercentage + '%';
            progressBar.innerText = progressPercentage + '%';
        }

        // Chama a função para atualizar a barra ao carregar a página
        updateProgressBar();