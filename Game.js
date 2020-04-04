const GAMES = {
  '56863965': {
    members: [{ id: '56863965', profile: {}, score: 0 }],
    hits: [{ userId: '56863965', tsId: '3|7|2019|3|0' }],
  },
};

module.exports = class Game {
  static async getGame(chatId) {
    let doc = GAMES[chatId];
    if (!doc) {
      GAMES[chatId] = { members: [], hits: [] };
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

  get hits() {
    return this.game.hits;
  }

  ensureMember(user) {
    const { id: userId, ...profile } = user;
    if (!this.members.find((m) => m.id === userId)) {
      this.members.push({
        id: userId,
        profile: { username: user.username },
        score: 0,
      });
    }
  }

  hasTsBeenClaimed(ts) {
    return !!this.hits.find(({ tsId }) => tsId === ts.toUniqueId());
  }

  addScore(userId, ts) {
    if (this.hasTsBeenClaimed(ts)) throw new Error('Timestamp claimed');
    const score = ts.score();
    this.members.find((m) => userId === m.id).score += score;
    this.hits.push({ userId, tsId: ts.toUniqueId() });
  }

  setScore(user, score) {
    this.ensureMember(user);
    this.members.find((m) => user.id === m.id).score = score;
  }
};
