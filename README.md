# 05-17_vizsgafeladatok

## 1.1 verzio fix

//8. feladat\
double napi_bev = 0;\
double osszes_perc = 0;\
List<double> percek = new List<double>();\
for (int i = 0; i < ViziB.Count; i++)\
{\
 percek.Add(Math.Ceiling(ViziB[i].hozott.TotalMinutes - ViziB[i].elvitt.TotalMinutes));\
 osszes_perc += (Math.Ceiling(percek[i] / 30));\
}\
napi_bev = osszes_perc * 2400;\
Console.WriteLine("8. feladat: A napi bevétel: {0} Ft", napi_bev);\
