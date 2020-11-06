const Game = require('./Game');

const MOVE_REGEX = /^(\d\d):(\d\d)$/;

module.exports = class App {
  static name = 'clock';

  static init({ storage }) {
    const app = new App({ storage });
    return {
      instance: app,
      commands: [
        {
          command: MOVE_REGEX,
          handler: (...args) => app.handleTime(...args),
        },
        {
          command: /^\/clock score$/,
          handler: (...args) => app.handleScore(...args),
        },
        {
          command: /^\/clock recalc$/,
          handler: (...args) => app.handleRecalc(...args),
        },
      ],
    };
  }

  constructor({ storage }) {
    this.storage = storage;
  }

  handleMove(game, message, match) {
    const d = new Date(message.createdAt);
    const { reply, state } = game.handleMove(
      message.user,
      new Game.Move({
        date: d,
        hours: d.getHours(),
        minutes: d.getMinutes(),
        messageMinutes: parseInt(match[2], 10),
        messageHours: parseInt(match[1], 10),
      })
    );

    return { reply, state };
  }

  handleTime(message, match) {
    const currentState = this.storage.getItem(message.chatId);
    if (!currentState) {
      this.handleRecalc(message);
      return;
    }
    const game = new Game(currentState);
    const { state, reply } = this.handleMove(game, message, match);

    this.storage.setItem(message.chatId, state);

    message.reply(
      `${reply}

${game.scoreboard()}`
    );
  }
  handleScore(message) {
    const currentState = this.storage.getItem(message.chatId);
    const game = new Game(currentState);
    message.reply(`\n${game.scoreboard()}`);
  }

  async handleRecalc(message) {
    message.reply(`recalculating :robot:`);
    const game = new Game();
    const msgs = await message.channel.getMessages();
    msgs.reverse();
    msgs.forEach(m => {
      const match = MOVE_REGEX.exec(m.content);
      if (!match) return;
      this.handleMove(game, m, match);
    });
    this.storage.setItem(message.chatId, game.state);
    message.reply(`\n${game.scoreboard()}`);
  }
};
