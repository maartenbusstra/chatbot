module.exports = class MemoryStore {
  db = {};

  getItem(key) {
    return this.db[key];
  }

  setItem(key, value) {
    this.db[key] = value;
    console.log('new db:');
    console.log(JSON.stringify(this.db, null, 2));
  }
};
