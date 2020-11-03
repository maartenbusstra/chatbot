module.exports = class Channel {
  constructor({
    id,
    getMessages = () => console.error('no reply handler given to message'),
  }) {
    this.id = id;
    this.getMessages = (...args) => getMessages(...args);
  }
};
