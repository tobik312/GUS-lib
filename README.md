# GUS-lib
Biblioteka umożliwia komunikację z API REGON (interfejsu udostępnionego przez portal GUS).
\
Jest zgodna z .NET standard, więc można jej używać niezależnie od platformy.

### Dostępne funkcje:
- Uzyskiwanie tokenu sesji
- Wyszukiwanie podmiotów:
  - [x] NIP
  - [x] KRS
  - [x] REGON
  - [x] Wiele NIP-ów
  - [x] Wiele KRS-ów
  - [x] Wiele REGON-ów
- Zestawianie raportów:
  - [ ] Pełny raport
  - [ ] Raport zbiorczy
- Uzyskiwanie informacji o
  - [x] stanie danych
  - [x] statusie sesji
  - [x] statusie usługi
  - [x] komunikaty usługi
  - [x] komunikaty/kody informacyjne
### Instalacja
Pakiet NuGet `Install-Package GUS.lib`
\
https://www.nuget.org/packages/GUS.lib
### Dokumentacja
Dokumentacja zawierająca szczgółowy opis interfejsu API dostępny jest na stronie
\
https://api.stat.gov.pl/Home/RegonApi
\
\
Dokumentacja biblioteki: `będzie dostępna później`
### Licencja
Prawa autorskie do kodu należą do Tobiasza Wilhelm (tobik312).\
Kod wydany jest na licencji MIT.
