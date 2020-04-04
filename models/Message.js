module.exports = class Message {
  constructor({ id, user, channelId, content, createdAt }) {
    this.id = id;
    this.user = user;
    this.content = content;
    this.channelId = channelId;
    this.createdAt = createdAt;
  }
};
