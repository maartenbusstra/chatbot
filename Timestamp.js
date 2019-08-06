module.exports = class Timestamp {
  constructor({ hours, minutes }) {
    this.hours = hours;
    this.minutes = minutes;
  }

  score() {
    if (this.hours !== this.minutes) return 0;
    if (this.hours === 0 || this.hours == 11 || this.hours === 22) return 2;
    return 1;
  }
};
