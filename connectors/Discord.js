const Discord = require('discord.js');
const Message = require('../lib/Message');
const Channel = require('../lib/Channel');

async function lots_of_messages_getter(channel, limit = 900000) {
  const sum_messages = [];
  let last_id;

  while (true) {
    const options = { limit: 100 };
    if (last_id) {
      options.before = last_id;
    }

    const messages = await channel.messages.fetch(options);
    sum_messages.push(...messages.array());
    last_id = messages.last().id;

    if (messages.size != 100 || sum_messages >= limit) {
      break;
    }
  }

  return sum_messages;
}

module.exports = class DiscordConnector {
  constructor({ token }) {
    this.token = token;
  }

  connect(handleMessage) {
    this.client = new Discord.Client();
    this.client.on('ready', () => {
      console.log(`Logged in as ${this.client.user.tag}!`);
    });

    this.client.on('message', msg => {
      if (msg.author.id === this.client.user.id) return;
      handleMessage(
        new Message({
          id: msg.id,
          user: {
            id: msg.author.id,
            name: msg.author.username,
          },
          channel: new Channel({
            id: msg.channel.id,
            getMessages: async () => {
              const msgs = await lots_of_messages_getter(msg.channel);
              return msgs.map(m => ({
                id: m.id,
                user: {
                  id: m.author.id,
                  name: m.author.username,
                },
                content: m.content,
                chatId: m.channel.id,
                createdAt: m.createdTimestamp,
              }));
            },
          }),
          content: msg.content,
          chatId: msg.channel.id,
          createdAt: msg.createdTimestamp,
          reply: content => msg.reply(content),
        })
      );
    });

    this.client.login(this.token);
  }
};
