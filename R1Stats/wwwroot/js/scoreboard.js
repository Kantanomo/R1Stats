var ScoreBoard = function ScoreBoard(element) {
    this.element_ = element;
    this.cards = [];
    this.nav = [];
    this.init();

};
window["ScoreBoard"] = ScoreBoard;

ScoreBoard.prototype.init = function() {
    this.cards = this.element_.querySelectorAll('.scoreboard');
    this.nav = this.element_.querySelectorAll('.nav-item');
    console.log(this.nav);
    var _this = this;

    this.nav.forEach(function(nav) {
        console.log(nav);
        nav.addEventListener("click",
            function() {
                _this.changeTab(nav.dataset["card"]);
            });
    });

    this.cards.forEach(function(card) {
        card.style.display = 'none';
    });
    this.cards[0].style.display = 'table';
}

ScoreBoard.prototype.changeTab = function (tab) {
    this.cards.forEach(function (card) {
        card.style.display = 'none';
        if (card.dataset["card"] === tab)
            card.style.display = 'table';
    });
    this.nav.forEach(function(nav) {
        nav.querySelector('a').classList.remove("active");
        if (nav.dataset["card"] === tab)
            nav.querySelector('a').classList.add("active");
    });
}


document.addEventListener("DOMContentLoaded",
    function () {
        componentHandler.register({
            constructor: ScoreBoard,
            classAsString: 'ScoreBoard',
            cssClass: 'scoreboard-container',
            widget: true
        });
    });