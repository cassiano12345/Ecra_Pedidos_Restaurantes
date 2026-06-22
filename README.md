### 📧Ercã de mesa e pedidos para restaurantes
O presente projeto apresenta uma solução para restaurantes e bares, em forma de ecrã apresenta para os cozinheiros e garçons a mesa e o pedido feito pelo cliente. O programa faz consultas a base de dados regularmente de forma a atualizar os pedidos a serem mostrados, quando um pedido é enviado ao cliente, no ecra o pedido aparece em verde, quando ainda não foi enviado aparece em vermelho como é possivel ver na imagem "imagem principal.png", também é possivel marcar o pedido como enviado, ou eliminar o pedido como é possivel ver na imagem "Detalhes pedido.png". O projeto foi feito em C#, e funciona com base de dados em Oracle, e Mysql, é possivel obter um exemplo da base de dados em Mysql na "Pasta -> Exemplo da BD em Mysql"

### Algumas funcionalidades a destacar

***Funções*** <br/> 

***Ficheiro->Ercã de pedidos restaurante c# ->Form1.cs***
- AjustarBotoes: A função serve para reorganizar os botões quando o formulário é redimensionado, ou seja quando a dimeção do programa muda, os botões são redimensionados para se ajustarem a nova dimensão do ecrã.
- CriarBotoes: quando o programa começa é logo feita uma consulta de pedidos de clientes presentes na base de dados, e a função CriarBotoes tem como objetivo apresentar esses pedidos como forma de botões, e quando se clica nos botões aparece o formulario M_Box, onde é possivel definir o pedido como enviado bem como eliminar o pedido.

***Ficheiro->Ercã de pedidos restaurante c# -> OracleChangeNotifier.cs***<br/>

- StartListening: A função destina a apresentar os pedidos dos clientes na base de dados.
- selecionar_sala: Na tabela que recebe os pedidos dos clientes, o valor da sala aparece em numerario, e é necessario fazer uma pesquisa pela designão da sala por extenso a outra tabela, ou seja esse é o objetivo da função selecionar_sala.
- Pedido_Executado: O objetivo da função é fazer um Update ao pedido do cliente na base de dados definindo com enviado, e posteriormente aparece no ecrã em verde.
- Delete_Pedido: O objetivo da função é de apagar pedidos de clientes na base de dados.
- StopListening: O objetivo da função é de parar a escuta de novos pedidos na base de dados.

***Variáveis*** <br/>
***Ficheiro->Ercã de pedidos restaurante c# ->Form1.cs***
- ConnectionString: É a variavel destinada a guardar os dados da conexão a base de dados.
- _oracleChangeNotifier: É uma variavel que guarda uma instancia da classe "OracleChangeNotifier", com ela é possivel chamar metodos como: StartListening(), selecionar_sala(), Pedido_Executado(), Delete_Pedido(), StopListening().
- dados: É uma variavel do tipo "MeusDados" da classe "MeusDados", que serve para guardar os dados dos pedidos dos clientes proveninetes da base da base de dados. <br/>

***Ficheiro->Ercã de pedidos restaurante c# -> OracleChangeNotifier.cs***
- Descricao: É uma variavel da classe "MeusDados", e serve para guardar a descrição do pedidos dos clientes
- mesa: É uma variavel da classe "MeusDados", e serve para guardar o numero da mesa dos clientes
- sala: É uma variavel da classe "MeusDados", e serve para guardar o numero da sala dos clientes, que mais tarde sera feita uma pesquisa pelo nome da sala na tabela pos_mesas.
- status: É uma variavel da classe "MeusDados", e serve para guardar o estado do pedido, ou seja se ja foi enviado o pedido aparece em verde, se não foi enviado o pedido aparece em vermelho.
- quantidade:É uma variavel da classe "MeusDados", e serve para guardar a quantidade de pedidos ou pratos pedidos pelo cliente.

***Imagens*** <br/>

***Página principal*** <br/>
O print a baixo mostra a página principal do programa, nele é possivel ver os a representação dos pedidos, com o nome das salas, o numero da mesa, e o nome dos pratos, os pedidos em verde significam que ja foram enviados ao cliente, os que estão em vermelho mostram que ainda estão a ser feitos. É possivel ver também um botão que foi criado para atualizar caso por algum motivo a atualização automatica falhe, e tem também o botão de sair(fechar a aplicação).


<p align="center">
  <img src="Imagem principal.png" alt="OpenMontage" width="700">
</p>

***Janela de detalhes*** <br/>
Ao clicar nos pedidos abre a janela a baixo onde é possivel ver os detalhes dos pedidos, bem como definir como executado(enviar ao cliente), e eliminar o pedido.
<p align="center">
  <img src="Detalhes pedido.png" alt="OpenMontage" width="700">
</p>
