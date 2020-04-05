const DiscordJS = require('discord.js');
const Message = require('../lib/Message');

module.exports = class Discord {
  constructor({ token }) {
    this.token = token;
  }

  connect(handleMessage) {
    this.client = new DiscordJS.Client();
    this.client.on('ready', () => {
      console.log(`Logged in as ${this.client.user.tag}!`);
    });

    this.client.on('message', (msg) => {
      if (msg.author.id === this.client.user.id) return;

      handleMessage(
        new Message({
          id: msg.id,
          user: {
            id: msg.author.id,
            name: msg.author.username,
          },
          content: msg.content,
          chatId: msg.channel.id,
          createdAt: msg.createdTimestamp,
          reply: (content) => msg.reply(content),
        }),
      );
    });

    this.client.login(this.token);
  }
};
