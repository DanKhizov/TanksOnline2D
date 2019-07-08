const PORT = process.env.PORT || 52300;
const io = require("socket.io")(PORT);

const Player = require("./Classes/Player");

const players = {};
const sockets = {};

io.on("connection", socket => {
    console.log("New connection");

    const player = new Player();
    const thisPlayerId = player.id;

    players[thisPlayerId] = player;
    sockets[thisPlayerId] = socket;

    console.log(players);

    socket.emit("register", { id: thisPlayerId });
    socket.emit("spawn", player);
    socket.broadcast.emit("spawn", player);

    for (const id in players) {
        if (players.hasOwnProperty(id)) {
            if (id !== thisPlayerId) socket.emit("spawn", players[id]);
        }
    }

    socket.on("updatePosition", newPosition => {
        console.log(newPosition);

        player.position.x = newPosition.x;
        player.position.y = newPosition.y;

        console.log(player.position);

        socket.broadcast.emit("updatePosition", player);
    });

    socket.on("disconnect", () => {
        console.log("A player has been disconnected");
        delete players[thisPlayerId];
        delete sockets[thisPlayerId];
        socket.broadcast.emit("disconnected", player);
    });
});

console.log(`Server is running on port ${PORT}`);
