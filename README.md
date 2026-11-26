# Minutnik

Minutnik Pomodoro zbudowany w Avalonia (.NET 10). Spełnia siedem zasad jednocześnie:
1. Czas pracy: 25 minut (edytowalny)
2. Czas przerwy: 5 minut (edytowalny)
3. Jeden przycisk Start/Pauza i jeden Reset
4. Widoczny licznik ukończonych cykli
5. Przerwa startuje automatycznie po pracy
6. Czas wyświetlany w formacie `mm:ss`
7. Interfejs po polsku, nazwy w kodzie po angielsku

---

## Liczby z lekcji 3a — 26.11.2026 — Antigravity (Claude Sonnet 4.5)

### Tokeny — pierwsza wiadomość (Tokenizer Playground, Xenova/gpt-4 / cl100k_base, 26.11.2026)

- **Po polsku:** 163 tokeny (461 znaków)
- **Po angielsku:** 102 tokeny (499 znaków)

### Okno kontekstu i maksymalna długość odpowiedzi

- Model: **Claude Sonnet 4.5**
- Okno kontekstu: **200 000 tokenów** (źródło: https://platform.claude.com/docs/en/about-claude/models/overview)
- Maksymalna długość odpowiedzi: **64 000 tokenów** (źródło: https://platform.claude.com/docs/en/about-claude/models/overview)

---

## Wynik próby z polem długości (kultura systemowa: pl-PL)

**Wpisano `25,5`:** Zapis poprawny — `double.TryParse` rozpoznało przecinek jako separator dziesiętny (kultura pl-PL). Minutnik ustawił się na 25 minut 30 sekund i ruszył.

**Wpisano `25.5`:** Zapis niepoprawny — `double.TryParse` nie rozpoznało kropki jako separatora dziesiętnego w kulturze pl-PL (tam kropka to separator tysięcy). Parsowanie się nie powiodło (`ok=False, val=0`), więc minutnik cofnął się do wartości domyślnej 25 minut.
