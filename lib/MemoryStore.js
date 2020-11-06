module.exports = class MemoryStore {
  constructor(state) {
    this.db = state || {};
  }

  getItem(key) {
    return this.db[key];
  }

  setItem(key, value) {
    this.db[key] = value;
  }
};
