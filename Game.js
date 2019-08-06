const GAMES = {
  '56863965': {
    members: [{ id: '56863965', profile: {}, score: 0 }],
  },
};

module.exports = class Game {
  static async getGame(chatId) {
    let doc = GAMES[chatId];
    if (!doc) {
      GAMES[chatId] = { members: [] };
      doc = GAMES[chatId];
    }
    const game = new Game(doc);
    return game;
  }

  constructor(game) {
    this.game = game;
  }

  get members() {
    return this.game.members;
  }

  ensureMember(userId, profile) {
    if (!this.members.find(m => m.id === userId)) {
      this.members.push({ id: userId, profile, score: 0 });
    }
  }

  addScore(member, score) {
    this.members.find(m => member === m.id).score += score;
    console.log(GAMES);
  }
};
