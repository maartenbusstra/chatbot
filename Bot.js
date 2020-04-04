const Game = require('./models/Game');

module.exports = class Bot {
  constructor({ service }) {
    this.service = service;
    this.init();
  }

  init() {
    this.service.subscribe((...args) => this.handleMessage(...args));
  }

  start() {
    this.service.start();
  }

  async handleMessage(message) {
    const state = this.service.getGameState(message.channelId);
    console.log(state);
    const game = new Game(state);
  }
};
