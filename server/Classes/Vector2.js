module.exports = class Vector2 {
    constructor(x = 0, y = 0) {
        this.x = x;
        this.y = y;
    }

    Magnitude() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
    }

    Normalized() {
        const magnitude = this.Magnitude();
        return new Vector2(this.x / magnitude, this.y / magnitude);
    }

    Distance(otherVector = Vector2) {
        const x = otherVector.x - this.x;
        const y = otherVector.y - this.y;

        return new Vector2(x, y).Magnitude();
    }

    ConsoleOutput() {
        return `(${this.x}, ${this.y})`;
    }
};
