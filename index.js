const Bot = require('./bot/Bot');
const Clock = require('./apps/clock');
const Discord = require('./connectors/Discord');

const discord = new Discord({
  token: '',
});

const memoryStore = {
  db: {},
  getItem(key) {
    return this.db[key];
  },
  setItem(key, value) {
    this.db[key] = value;
  },
};

const bot = new Bot({
  connector: discord,
  // storage: new discord.MessageStore(),
  storage: memoryStore,
  apps: [Clock],
});

bot.start();
