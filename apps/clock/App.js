const Game = require('./Game');

module.exports = class App {
  static name = 'clock';

  static init({ storage }) {
    const app = new App({ storage });
    return {
      instance: app,
      commands: [
        {
          command: /^(\d\d):(\d\d)$/,
          handler: (...args) => app.handleTime(...args),
        },
        {
          command: /^\/clock score$/,
          handler: (...args) => app.handleScore(...args),
        },
      ],
    };
  }

  constructor({ storage }) {
    this.storage = storage;
  }

  handleTime(message, match) {
    const currentState = this.storage.getItem(message.chatId);
    const game = new Game(currentState);

    const d = new Date(message.createdAt);
    const { reply, state } = game.handleMove(
      message.user,
      new Game.Move({
        date: d,
        hours: d.getHours(),
        minutes: d.getMinutes(),
        messageMinutes: parseInt(match[2], 10),
        messageHours: parseInt(match[1], 10),
      }),
    );

    this.storage.setItem(message.chatId, state);

    message.reply(
      `${reply}

${game.scoreboard()}`,
    );
  }
  handleScore(message) {
    const currentState = this.storage.getItem(message.chatId);
    const game = new Game(currentState);
    message.reply(`\n${game.scoreboard()}`);
  }
};
