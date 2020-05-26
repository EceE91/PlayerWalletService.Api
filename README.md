# PlayerWalletService.Api

<h1>Main Technologies</h1>
<ul>
<li>.Net Core 3.1</li>
<li>EF Core</li>
<li>Swagger</li>
<li>XUnit tests with Moq</li>
</ul>

<p>To achieve standartization Every response returns the format below</p>
<p>{
    "code": int,
    "done": bool,
    "data": object
}</p>

<p>To see the api endpoints you can simply enter localhost:port/swagger</p>
<p>For testing purposes I added seed data (Transactions-players-wallets)</p>
