const Bot = require('./bot/Bot');
const Clock = require('./apps/clock');
const Discord = require('./connectors/Discord');

const discord = new Discord({
  token: '',
});

const bot = new Bot({
  connector: discord,
  // storage: new discord.MessageStore(),
  apps: [Clock],
});

bot.start();
