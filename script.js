// ================
// Smooth scroll from hero button to game
// ================

const scrollToGameBtn = document.getElementById("scrollToGameBtn");
const gameSection = document.getElementById("game");

if (scrollToGameBtn && gameSection) {
  scrollToGameBtn.addEventListener("click", () => {
    gameSection.scrollIntoView({ behavior: "smooth" });
  });
}

// ================
// DOM References
// ================

const gamePanel = document.getElementById("gamePanel");
const panelMessage = document.getElementById("panelMessage");
const startBtn = document.getElementById("startBtn");
const resetBtn = document.getElementById("resetBtn");

const currentTimeEl = document.getElementById("currentTime");
const bestTimeEl = document.getElementById("bestTime");
const bestLabelEl = document.getElementById("bestLabel");
const historyList = document.getElementById("historyList");
const feedbackText = document.getElementById("feedbackText");

// ================
// State
// ================

// "idle"   -> waiting to start
// "waiting"-> user must wait (yellow)
// "ready"  -> green, user should click
// "lock"   -> short lock after too-soon
let state = "idle";

let timeoutId = null;
let startTime = null;

const STORAGE_KEY = "reactrush-reaction-data";
let bestTime = null;
let history = []; // { time, timestamp }

// base colors
const COLOR_IDLE = "#050616";
const COLOR_WAITING = "#ffb300"; // yellow
const COLOR_READY = "#00e676";   // green
const COLOR_TOO_SOON = "#ff5252"; // red

// ================
// Storage helpers
// ================

function loadData() {
  try {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (!raw) return;
    const parsed = JSON.parse(raw);
    if (typeof parsed.bestTime === "number") {
      bestTime = parsed.bestTime;
    }
    if (Array.isArray(parsed.history)) {
      history = parsed.history;
    }
  } catch (e) {
    // ignore
  }
}

function saveData() {
  try {
    const payload = { bestTime, history };
    localStorage.setItem(STORAGE_KEY, JSON.stringify(payload));
  } catch (e) {
    // ignore
  }
}

// ================
// UI helpers
// ================

function setPanelVisual(mode) {
  // mode: "idle" | "waiting" | "ready" | "too-soon"
  if (!gamePanel) return;

  if (mode === "waiting") {
    gamePanel.style.backgroundColor = COLOR_WAITING;
    panelMessage.textContent = "Get ready... wait for green.";
    feedbackText.textContent = "Don't tap yet. Wait for green!";
  } else if (mode === "ready") {
    gamePanel.style.backgroundColor = COLOR_READY;
    panelMessage.textContent = "Tap now!";
    feedbackText.textContent = "Go, go, go!";
  } else if (mode === "too-soon") {
    gamePanel.style.backgroundColor = COLOR_TOO_SOON;
    panelMessage.textContent = "Too soon!";
    feedbackText.textContent =
      "You clicked before it turned green. Try again.";
  } else {
    // idle
    gamePanel.style.backgroundColor = COLOR_IDLE;
    panelMessage.textContent = "Press Start to begin";
    feedbackText.textContent =
      "When the panel turns green, tap as fast as you can.";
  }
}

function renderStats() {
  if (bestTime != null) {
    bestTimeEl.textContent = bestTime.toFixed(0);
    if (bestTime < 150) {
      bestLabelEl.textContent = "Lightning fast! Pro-level reactions.";
    } else if (bestTime < 250) {
      bestLabelEl.textContent = "Very good. Above average speed.";
    } else if (bestTime < 350) {
      bestLabelEl.textContent = "Decent. With practice, you’ll improve.";
    } else {
      bestLabelEl.textContent = "Quite slow. Try to stay focused and relaxed.";
    }
  } else {
    bestTimeEl.textContent = "—";
    bestLabelEl.textContent = "No data yet. Play a round!";
  }

  historyList.innerHTML = "";
  if (history.length === 0) {
    const li = document.createElement("li");
    li.textContent = "No attempts yet.";
    historyList.appendChild(li);
  } else {
    history
      .slice()
      .reverse()
      .slice(0, 6)
      .forEach((entry, index) => {
        const li = document.createElement("li");
        const labelSpan = document.createElement("span");
        labelSpan.textContent = `#${history.length - index}`;

        const timeSpan = document.createElement("span");
        timeSpan.textContent = `${entry.time.toFixed(0)} ms`;

        li.appendChild(labelSpan);
        li.appendChild(timeSpan);
        historyList.appendChild(li);
      });
  }
}

function recordReaction(durationMs) {
  currentTimeEl.textContent = durationMs.toFixed(0);

  if (bestTime == null || durationMs < bestTime) {
    bestTime = durationMs;
  }

  history.push({ time: durationMs, timestamp: Date.now() });
  if (history.length > 30) {
    history = history.slice(history.length - 30);
  }

  renderStats();
  saveData();
}

// ================
// Game logic
// ================

function startGame() {
  // prevent restarting while already running
  if (state === "waiting" || state === "ready") return;

  clearTimeout(timeoutId);
  startTime = null;
  currentTimeEl.textContent = "—";

  state = "waiting";
  setPanelVisual("waiting");

  // Random delay: 0.8s – 2.0s
  const delay = 800 + Math.random() * 1200;

  timeoutId = setTimeout(() => {
    startTime = performance.now();
    state = "ready";
    setPanelVisual("ready");
  }, delay);
}

function handlePanelClick() {
  if (state === "waiting") {
    // clicked too early
    clearTimeout(timeoutId);
    startTime = null;
    state = "lock";
    setPanelVisual("too-soon");
    currentTimeEl.textContent = "—";

    setTimeout(() => {
      state = "idle";
      setPanelVisual("idle");
    }, 800);
    return;
  }

  if (state === "ready") {
    const endTime = performance.now();
    const duration = endTime - startTime;
    state = "idle";
    setPanelVisual("idle");
    recordReaction(duration);
    return;
  }

  // if idle or lock: ignore clicks
}

// ================
// Event bindings
// ================

if (startBtn) {
  startBtn.addEventListener("click", () => {
    startGame();
  });
}

if (gamePanel) {
  gamePanel.addEventListener("click", () => {
    handlePanelClick();
  });
}

if (resetBtn) {
  resetBtn.addEventListener("click", () => {
    if (!confirm("Reset best time and history?")) return;
    bestTime = null;
    history = [];
    currentTimeEl.textContent = "—";
    state = "idle";
    setPanelVisual("idle");
    renderStats();
    saveData();
  });
}

// ================
// Init
// ================

loadData();
state = "idle";
setPanelVisual("idle");
renderStats();
