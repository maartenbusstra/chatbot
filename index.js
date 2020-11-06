require('dotenv').config();
const MemoryStore = require('./lib/MemoryStore');
const Bot = require('./bot/Bot');
const Clock = require('./apps/clock');
const Discord = require('./connectors/Discord');

const discord = new Discord({
  token: process.env.DISCORD_TOKEN,
});

const bot = new Bot({
  connector: discord,
  storage: new MemoryStore(),
  apps: [Clock],
});

bot.start();
