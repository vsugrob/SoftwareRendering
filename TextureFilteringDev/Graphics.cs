using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common3d;
using Math3d;

namespace TextureFilteringDev {
	public static class Graphics {
		#region Settings
		public static bool ShowLods = false;
		#endregion Settings

		public static void DrawTexturedTriangle ( this HdrBuffer image, double3 [] pts, double2 [] t,
			Sampler2d sampler, double tpp )
		{
			int bottomIdx, midIdx, topIdx, separateIdx, coincidentIdx;
			Classify3Result res = Math3.Classify3 ( new [] { pts [0].y, pts [1].y, pts [2].y },
				out topIdx, out midIdx, out bottomIdx, out separateIdx, out coincidentIdx );

			if ( res == Classify3Result.AllCoincident ) {
				int x = ( int ) Math.Round ( pts [0].x, MidpointRounding.AwayFromZero );
				int y = ( int ) Math.Round ( pts [0].y, MidpointRounding.AwayFromZero );
				image.Values [x, y] = sampler.GetColor ( t [0] );

				return;
			} else if ( res == Classify3Result.HasCoincidence ) {
				double3 separatePoint = pts [separateIdx];
				double3 midPoint = pts [midIdx];
				double3 coincidentPoint = pts [coincidentIdx];
				double2 separateT = t [separateIdx];
				double2 midT = t [midIdx];
				double2 coincidentT = t [coincidentIdx];

				image.DrawXAlignedTexturedTriangle ( separatePoint, midPoint.x, coincidentPoint.x, midPoint.y,
				    midPoint.z, coincidentPoint.z,
				    separateT, midT, coincidentT,
					sampler, tpp );
				//image.DrawXAlignedHollowTriangle ( separatePoint, midPoint.x, coincidentPoint.x, midPoint.y, double3.UnitY );
			} else {
				double3 topPoint = pts [topIdx];
				double3 midPoint = pts [midIdx];
				double3 bottomPoint = pts [bottomIdx];
				double k = ( midPoint.y - bottomPoint.y ) / ( topPoint.y - bottomPoint.y );
				double x2 = k.Lerp ( bottomPoint.x, topPoint.x );

				double2 topT = t [topIdx];
				double2 midT = t [midIdx];
				double2 bottomT = t [bottomIdx];
				double zInv = k.Lerp ( 1 / bottomPoint.z, 1 / topPoint.z );
				double z2 = 1 / zInv;
				double2 coincidentT = k.ZLerp ( bottomT, topT, bottomPoint.z, topPoint.z, zInv );

				image.DrawXAlignedTexturedTriangle ( bottomPoint, midPoint.x, x2, midPoint.y,
				    midPoint.z, z2,
				    bottomT, midT, coincidentT,
					sampler, tpp );
				image.DrawXAlignedTexturedTriangle ( topPoint, midPoint.x, x2, midPoint.y, 
				    midPoint.z, z2,
				    topT, midT, coincidentT,
					sampler, tpp );

				//image.DrawXAlignedHollowTriangle ( bottomPoint, midPoint.x, x2, midPoint.y, double3.UnitX );
				//image.DrawXAlignedHollowTriangle ( topPoint, midPoint.x, x2, midPoint.y, double3.UnitX );

				//double3 white = new double3 ( 1, 1, 1 );
				//Func <double3, double3, double3> blendFunc = ( bg, fg ) => bg == white ? double3.UnitX : ( bg == double3.UnitY ? double3.UnitZ : double3.UnitY );

				//image.DrawYSteppingLine ( bottomPoint, new double2 ( midPoint.x, midPoint.y ), double3.UnitX, true, false, blendFunc );
				//image.DrawYSteppingLine ( bottomPoint, new double2 ( x2, midPoint.y ), double3.UnitX, false, false, blendFunc );
				//image.DrawYSteppingLine ( topPoint, new double2 ( midPoint.x, midPoint.y ), double3.UnitX, true, true, blendFunc );
				//image.DrawYSteppingLine ( topPoint, new double2 ( x2, midPoint.y ), double3.UnitX, false, true, blendFunc );
			}
		}

		//public static void DrawXAlignedTexturedTriangle ( this HdrBuffer image,
		//    double3 p0, double x1, double x2, double sideY,
		//    double z1, double z2,
		//    double2 t0, double2 t1, double2 t2,
		//    Sampler2d sampler,
		//    bool includeSide = true )
		//{
		//    int h1 = image.Size.height - 1;
		//    int w1 = image.Size.width - 1;

		//    if ( ( p0.y < 0 && sideY < 0 ) ||
		//         ( p0.y > h1 && sideY > h1 ) ||
		//         ( p0.x < 0 && x1 < 0 && x2 < 0 ) ||
		//         ( p0.x > w1 && x1 > w1 && x2 > w1 ) )
		//        return;

		//    double dy = ( sideY - p0.y );
		//    double dx1 = ( x1 - p0.x );
		//    double dx2 = ( x2 - p0.x );
		//    int stepY = Math.Sign ( dy );
		//    dy = Math.Abs ( dy );
		//    double ik1 = dx1 / dy;
		//    double ik2 = dx2 / dy;
		//    int startY = ( int ) Math.Round ( p0.y );
		//    int endY = ( int ) Math.Round ( sideY );
		//    double curX1 = p0.x, curX2 = p0.x;

		//    if ( startY < 0 ) {
		//        int displY = 0 - startY;
		//        curX1 += ik1 * displY;
		//        curX2 += ik2 * displY;
		//        startY = 0;
		//    } else if ( startY > h1 ) {
		//        int displY = h1 - startY;
		//        curX1 += ik1 * displY;
		//        curX2 += ik2 * displY;
		//        startY = h1;
		//    }

		//    if ( endY < 0 ) {
		//        endY = 0;
		//        includeSide = true;
		//    } else if ( endY > h1 ) {
		//        endY = h1;
		//        includeSide = true;
		//    }

		//    int numLines = Math.Abs ( endY - startY );
		//    bool reflectX = x1 > x2;

		//    if ( includeSide )
		//        numLines++;

		//    ik1 = dx1 / numLines;
		//    ik2 = dx2 / numLines;

		//    double2 t1DivZ1Step = ( t1 / z1 - t0 / p0.z ) / numLines;
		//    double2 t2DivZ2Step = ( t2 / z2 - t0 / p0.z ) / numLines;
		//    double2 curT1DivZ1 = t0 / p0.z, curT2DivZ2 = t0 / p0.z;

		//    double z1InvStep = ( 1 / z1 - 1 / p0.z ) / numLines;
		//    double z2InvStep = ( 1 / z2 - 1 / p0.z ) / numLines;
		//    double curZ1Inv = 1 / p0.z, curZ2Inv = 1 / p0.z;

		//    double z1Step = ( z1 - p0.z ) / numLines;
		//    double z2Step = ( z2 - p0.z ) / numLines;
		//    double curZ1 = p0.z, curZ2 = p0.z;

		//    for ( int i = 0, curY = startY ; i < numLines ; i++, curY += stepY ) {
		//        // FIXIT: fix rounding (clearly seen on sides like ( 10, 100, 10, 101 )
		//        int leftX  = ( int ) Math.Round ( curX1, MidpointRounding.AwayFromZero ),
		//            rightX = ( int ) Math.Round ( curX2, MidpointRounding.AwayFromZero );

		//        if ( ( leftX >= 0 || rightX >= 0 ) &&
		//             ( leftX <= w1 || rightX <= w1 ) ) {
		//            double2 leftTDivZ  = curT1DivZ1,
		//                    rightTDivZ = curT2DivZ2;
		//            double leftZInv  = curZ1Inv,
		//                   rightZInv = curZ2Inv;
		//            double leftZ = 1 / curZ1Inv;
		//            double rightZ = 1 / curZ2Inv;

		//            if ( reflectX ) {
		//                Util.Swap ( ref leftX, ref rightX );
		//                Util.Swap ( ref leftTDivZ, ref rightTDivZ );
		//                Util.Swap ( ref leftZInv, ref rightZInv );
		//                Util.Swap ( ref leftZ, ref rightZ );
		//            }

		//            int lineWidth = rightX - leftX;

		//            double2 tLeft = leftTDivZ / leftZInv;
		//            double2 tRight = rightTDivZ / rightZInv;

		//            double2 tDivZHorzStep = ( tRight / rightZ - tLeft / leftZ ) / lineWidth;
		//            double2 tDivZHorz = tLeft / leftZ;

		//            double zInvHorzStep = ( 1 / rightZ - 1 / leftZ ) / lineWidth;
		//            double zInvHorz = 1 / leftZ;

		//            double zHorzStep = ( rightZ - leftZ ) / lineWidth;
		//            double zHorz = leftZ;

		//            if ( leftX < 0 ) {
		//                tDivZHorz += ( 0 - leftX ) * tDivZHorzStep;
		//                zInvHorz += ( 0 - leftX ) * zInvHorzStep;
		//                zHorz += ( 0 - leftX ) * zHorzStep;
		//                leftX = 0;
		//            }
				
		//            if ( rightX > w1 )
		//                rightX = w1;

		//            for ( int x = leftX ; x <= rightX ; x++ ) {
		//                double2 t = tDivZHorz / zInvHorz;
		//                image.Values [x, curY] = sampler.GetColor ( t );
		//                //image.Values [x, curY] = new double3 ( t, 0 );
		//                //image.Values [x, curY] = Math.Round ( ( 1 - zHorz / 3.6 ) * 20 ) / 20;
		//                //image.Values [x, curY] = Math.Round ( ( 1 - ( 1 / zInvHorz ) / 3.6 ) * 20 ) / 20;
		//                tDivZHorz += tDivZHorzStep;
		//                zInvHorz += zInvHorzStep;
		//                zHorz += zHorzStep;
		//            }
		//        }

		//        curX1 += ik1;
		//        curX2 += ik2;
		//        curT1DivZ1 += t1DivZ1Step;
		//        curT2DivZ2 += t2DivZ2Step;
		//        curZ1Inv += z1InvStep;
		//        curZ2Inv += z2InvStep;
		//        curZ1 += z1Step;
		//        curZ2 += z2Step;
		//    }
		//}

		//public static void DrawXAlignedTexturedTriangle ( this HdrBuffer image,
		//    double3 p0, double x1, double x2, double sideY,
		//    double z1, double z2,
		//    double2 t0, double2 t1, double2 t2,
		//    Sampler2d sampler,
		//    bool includeSide = true )
		//{
		//    int h1 = image.Size.height - 1;
		//    int w1 = image.Size.width - 1;

		//    includeSide = false;

		//    if ( ( p0.y < 0 && sideY < 0 ) ||
		//         ( p0.y > h1 && sideY > h1 ) ||
		//         ( p0.x < 0 && x1 < 0 && x2 < 0 ) ||
		//         ( p0.x > w1 && x1 > w1 && x2 > w1 ) )
		//        return;

		//    double dy = ( sideY - p0.y );
		//    double dx1 = ( x1 - p0.x );
		//    double dx2 = ( x2 - p0.x );
		//    int stepY = Math.Sign ( dy );
		//    dy = Math.Abs ( dy );
		//    double ik1 = dx1 / dy;
		//    double ik2 = dx2 / dy;
		//    double dStartY = p0.y;
		//    double dEndY = sideY;
		//    double curX1 = p0.x, curX2 = p0.x;

		//    if ( dStartY < 0 ) {
		//        double displY = 0 - dStartY;
		//        curX1 += ik1 * displY;
		//        curX2 += ik2 * displY;
		//        dStartY = 0;
		//    } else if ( dStartY > h1 ) {
		//        double displY = h1 - dStartY;
		//        curX1 += ik1 * displY;
		//        curX2 += ik2 * displY;
		//        dStartY = h1;
		//    }

		//    if ( dEndY < 0 ) {
		//        double displY = 0 - dEndY;
		//        x1 -= ik1 * displY;
		//        x2 -= ik2 * displY;
		//        dEndY = 0;
		//        includeSide = true;
		//    } else if ( dEndY > h1 ) {
		//        double displY = h1 - dEndY;
		//        x1 -= ik1 * displY;
		//        x2 -= ik2 * displY;
		//        dEndY = h1;
		//        includeSide = true;
		//    }

		//    int startY = ( int ) Math.Round ( dStartY );
		//    int endY = ( int ) Math.Round ( dEndY );

		//    int numLines = Math.Abs ( endY - startY );
		//    bool reflectX = x1 > x2;

		//    if ( includeSide )
		//        numLines++;

		//    //ik1 = dx1 / numLines;
		//    //ik2 = dx2 / numLines;
		//    ik1 = ( x1 - curX1 ) / numLines;
		//    ik2 = ( x2 - curX2 ) / numLines;

		//    double2 t1DivZ1Step = ( t1 / z1 - t0 / p0.z ) / numLines;
		//    double2 t2DivZ2Step = ( t2 / z2 - t0 / p0.z ) / numLines;
		//    double2 curT1DivZ1 = t0 / p0.z, curT2DivZ2 = t0 / p0.z;

		//    double z1InvStep = ( 1 / z1 - 1 / p0.z ) / numLines;
		//    double z2InvStep = ( 1 / z2 - 1 / p0.z ) / numLines;
		//    double curZ1Inv = 1 / p0.z, curZ2Inv = 1 / p0.z;

		//    double z1Step = ( z1 - p0.z ) / numLines;
		//    double z2Step = ( z2 - p0.z ) / numLines;
		//    double curZ1 = p0.z, curZ2 = p0.z;

		//    for ( int i = 0, curY = startY ; i < numLines ; i++, curY += stepY ) {
		//        // FIXIT: fix rounding (clearly seen on sides like ( 10, 100, 10, 101 )
		//        //int leftX  = ( int ) Math.Round ( curX1, MidpointRounding.AwayFromZero ),
		//        //    rightX = ( int ) Math.Round ( curX2, MidpointRounding.AwayFromZero );
		//        double leftX = curX1,
		//               rightX = curX2;

		//        if ( ( leftX >= 0 || rightX >= 0 ) &&
		//             ( leftX <= w1 || rightX <= w1 ) ) {
		//            double2 leftTDivZ  = curT1DivZ1,
		//                    rightTDivZ = curT2DivZ2;
		//            double leftZInv  = curZ1Inv,
		//                   rightZInv = curZ2Inv;
		//            double leftZ = 1 / curZ1Inv;
		//            double rightZ = 1 / curZ2Inv;

		//            if ( reflectX ) {
		//                Util.Swap ( ref leftX, ref rightX );
		//                Util.Swap ( ref leftTDivZ, ref rightTDivZ );
		//                Util.Swap ( ref leftZInv, ref rightZInv );
		//                Util.Swap ( ref leftZ, ref rightZ );
		//            }

		//            double lineWidth = rightX - leftX;

		//            double2 tLeft = leftTDivZ / leftZInv;
		//            double2 tRight = rightTDivZ / rightZInv;

		//            double2 tDivZHorzStep = ( tRight / rightZ - tLeft / leftZ ) / lineWidth;
		//            double2 tDivZHorz = tLeft / leftZ;

		//            double zInvHorzStep = ( 1 / rightZ - 1 / leftZ ) / lineWidth;
		//            double zInvHorz = 1 / leftZ;

		//            double zHorzStep = ( rightZ - leftZ ) / lineWidth;
		//            double zHorz = leftZ;

		//            if ( leftX < 0 ) {
		//                tDivZHorz += ( 0 - leftX ) * tDivZHorzStep;
		//                zInvHorz += ( 0 - leftX ) * zInvHorzStep;
		//                zHorz += ( 0 - leftX ) * zHorzStep;
		//                leftX = 0;
		//            }
					
		//            if ( rightX > w1 )
		//                rightX = w1;

		//            for ( int x = ( int ) leftX ; x <= rightX ; x++ ) {
		//                double2 t = tDivZHorz / zInvHorz;
		//                image.Values [x, curY] = sampler.GetColor ( t );
		//                //image.Values [x, curY] = new double3 ( t, 0 );
		//                //image.Values [x, curY] = Math.Round ( ( 1 - zHorz / 3.6 ) * 20 ) / 20;
		//                //image.Values [x, curY] = Math.Round ( ( 1 - ( 1 / zInvHorz ) / 3.6 ) * 20 ) / 20;
		//                tDivZHorz += tDivZHorzStep;
		//                zInvHorz += zInvHorzStep;
		//                zHorz += zHorzStep;
		//            }
		//        }

		//        curX1 += ik1;
		//        curX2 += ik2;
		//        curT1DivZ1 += t1DivZ1Step;
		//        curT2DivZ2 += t2DivZ2Step;
		//        curZ1Inv += z1InvStep;
		//        curZ2Inv += z2InvStep;
		//        curZ1 += z1Step;
		//        curZ2 += z2Step;
		//    }
		//}

		//public static void DrawXAlignedTexturedTriangle ( this HdrBuffer image,
		//    double3 p0, double x1, double x2, double sideY,
		//    double z1, double z2,
		//    double2 t0, double2 t1, double2 t2,
		//    Sampler2d sampler,
		//    bool includeSide = true )
		//{
		//    int h1 = image.Size.height - 1;
		//    int w1 = image.Size.width - 1;

		//    includeSide = false;

		//    if ( ( p0.y < 0 && sideY < 0 ) ||
		//         ( p0.y > h1 && sideY > h1 ) ||
		//         ( p0.x < 0 && x1 < 0 && x2 < 0 ) ||
		//         ( p0.x > w1 && x1 > w1 && x2 > w1 ) )
		//        return;

		//    double dy = sideY - p0.y;
		//    int stepY = Math.Sign ( dy );
		//    dy = Math.Abs ( dy );
		//    double startY = p0.y;
		//    double endY = sideY;

		//    double startX1 = p0.x;
		//    double startX2 = p0.x;
		//    double stepX1 = ( x1 - p0.x ) / dy;
		//    double stepX2 = ( x2 - p0.x ) / dy;

		//    double w05 = w1 + 0.5, h05 = h1 + 0.5;	// is h05 needed?

		//    if ( startY < 0 ) {
		//        double displY = 0 - startY;
		//        startX1 += displY * stepX1;
		//        startX2 += displY * stepX2;
		//        startY = 0;
		//    } else if ( startY > h1 ) {
		//        double displY = h1 - startY;
		//        startX1 += displY * stepX1;
		//        startX2 += displY * stepX2;
		//        startY = h1;
		//    }

		//    if ( endY < 0 )
		//        endY = 0;
		//    else if ( endY > h1 )
		//        endY = h1;

		//    int iY = ( int ) ( startY + 0.5 );
		//    //double roundDisplY = iY - startY;
		//    //double curX1 = startX1 + roundDisplY * stepX1;
		//    //double curX2 = startX2 + roundDisplY * stepX2;
		//    double curX1 = startX1;
		//    double curX2 = startX2;

		//    int iEndY = ( int ) ( endY + 0.5 );
		//    int numRows = Math.Abs ( iEndY - iY ) + 1;

		//    for ( int r = 0 ; r < numRows ; r++ ) {
		//        if ( ( curX1 >= -0.5 || curX2 >= -0.5 ) &&
		//             ( curX1 < w05 || curX2 < w05 ) )
		//        {
		//            int iX1 = ( int ) ( curX1 + 0.5 );
		//            int iX2 = ( int ) ( curX2 + 0.5 );

		//            // <tmp>
		//            iX1 = iX1.Clamp ( 0, w1 );
		//            iX2 = iX2.Clamp ( 0, w1 );
		//            int lx = iX1, rx = iX2;

		//            image.Values [iX1, iY] = double3.UnitX;
		//            image.Values [iX2, iY] = double3.UnitX;

		//            //if ( lx > rx )
		//            //    Util.Swap ( ref lx, ref rx );

		//            //for ( int c = lx ; c <= rx ; c++ )
		//            //    image.Values [c, iY] = double3.UnitX;

		//            // </tmp>
		//        }

		//        iY += stepY;
		//        curX1 += stepX1;
		//        curX2 += stepX2;
		//    }
		//}

		public static void DrawXAlignedHollowTriangle ( this HdrBuffer image,
			double2 p0, double x1, double x2, double sideY, double3 color )
		{
			int ix0 = ( int ) ( p0.x + 0.5 );
			int iy0 = ( int ) ( p0.y + 0.5 );
			int ix1 = ( int ) ( x1 + 0.5 );
			int ix2 = ( int ) ( x2 + 0.5 );
			int iSideY = ( int ) ( sideY + 0.5 );

			image.DrawLineBresenham ( ix0, iy0, ix1, iSideY, color );
			image.DrawLineBresenham ( ix0, iy0, ix2, iSideY, color );
			image.DrawLineBresenham ( ix1, iSideY, ix2, iSideY, color );
		}

		public static void DrawYSteppingLine ( this HdrBuffer image, double2 p0, double2 p1,
			double3 color, bool plotFirst = true, bool plotLast = true, Func <double3, double3, double3> blendFunc = null )
		{
			int w1 = image.Size.width - 1,
				h1 = image.Size.height - 1;
			double dy = p1.y - p0.y;
			int stepY = Math.Sign ( dy );
			dy = Math.Abs ( dy );
			double ik = ( p1.x - p0.x ) / dy;
			int ix, iy;

			if ( p0.y < 0 ) {
				p0.x += ik * ( 0 - p0.y );
				iy = 0;
			} else if ( p0.y > h1 ) {
				p0.x += ik * ( p0.y - h1 );
				iy = h1;
			} else {
				int nextIntY = ( int ) p0.y;

				if ( nextIntY != p0.y ) {
					nextIntY = p0.y.NextIntAlongDir ( stepY );

					if ( plotFirst ) {
						int curIntY = p0.y.PreciseRound ();

						if ( curIntY != nextIntY ) {
							ix = p0.x.PreciseRound ();

							if ( ix > 0 && ix < image.Size.width )
								image.Values [ix, curIntY] = blendFunc == null ? color : blendFunc ( image.Values [ix, curIntY], color );
						}
					}

					p0.x += ik * Math.Abs ( nextIntY - p0.y );
				}

				iy = nextIntY;
			}

			if ( p1.y > h1 )
				p1.y = h1;
			else if ( p1.y < 0 )
				p1.y = 0;
			else {
				int prevIntLastY = ( int ) p1.y;

				if ( prevIntLastY != p1.y ) {
					prevIntLastY = p1.y.PrevIntAlongDir ( stepY );

					if ( plotLast ) {
						int curIntLastY = p1.y.PreciseRound ();

						if ( curIntLastY != prevIntLastY ) {
							ix = p1.x.PreciseRound ();

							if ( ix > 0 && ix < image.Size.width )
								image.Values [ix, curIntLastY] = blendFunc == null ? color : blendFunc ( image.Values [ix, curIntLastY], color );
						}
					}
				}

				p1.y = prevIntLastY;
			}

			double curX = p0.x;
			int endY = ( int ) p1.y;
			int numRows = Math.Abs ( endY - iy ) + 1;

			for ( int r = 0 ; r < numRows ; r++ ) {
				ix = curX.PreciseRound ();

				if ( ix > 0 && ix < image.Size.width )
					image.Values [ix, iy] = blendFunc == null ? color : blendFunc ( image.Values [ix, iy], color );

				curX += ik;
				iy += stepY;
			}
		}

		public static void DrawXAlignedTexturedTriangle ( this HdrBuffer image,
		    double3 pStart, double endX1, double endX2, double sideY,
		    double endZ1, double endZ2,
		    double2 tStart, double2 endT1, double2 endT2,
		    Sampler2d sampler, double tpp )
		{
			int w1 = image.Size.width - 1,
				h1 = image.Size.height - 1;
			
		    if ( ( pStart.y < 0 && sideY < 0 ) ||
		         ( pStart.y > h1 && sideY > h1 ) ||
		         ( pStart.x < 0 && endX1 < 0 && endX2 < 0 ) ||
		         ( pStart.x > w1 && endX1 > w1 && endX2 > w1 ) )
		        return;

			if ( endX1 > endX2 ) {
				Util.Swap ( ref endX1, ref endX2 );
				Util.Swap ( ref endZ1, ref endZ2 );
				Util.Swap ( ref endT1, ref endT2 );
			}

			double dy = Math.Abs ( sideY - pStart.y );
			var x1Lerp = pStart.x.LerpTo ( endX1, dy ).GetInterpolator ();
			var x2Lerp = pStart.x.LerpTo ( endX2, dy ).GetInterpolator ();
			var z1Lerp = pStart.z.ZLerpTo ( endZ1, dy, pStart.z, endZ1 ).GetInterpolator ();
			var z2Lerp = pStart.z.ZLerpTo ( endZ2, dy, pStart.z, endZ2 ).GetInterpolator ();
			var t1Lerp = tStart.ZLerpTo ( endT1, dy, pStart.z, endZ1 ).GetInterpolator ();
			var t2Lerp = tStart.ZLerpTo ( endT2, dy, pStart.z, endZ2 ).GetInterpolator ();
			var yRasterizer = Rasterizer.Create <object> ( 0, h1, pStart.y, sideY, null,
				x1Lerp, x2Lerp,
				z1Lerp, z2Lerp,
				t1Lerp, t2Lerp
			);
			
			foreach ( var ySample in yRasterizer ) {
				int iy = ySample.Item1.PreciseRound ();
				double x1 = x1Lerp.Current;
				double x2 = x2Lerp.Current;
				double z1 = z1Lerp.Current;
				double z2 = z2Lerp.Current;
				double2 t1 = t1Lerp.Current;
				double2 t2 = t2Lerp.Current;

				double dx = Math.Abs ( x2 - x1 );

				if ( dx == 0 )
					continue;	// FIXIT: draw 1 pixel or...

				var zLerp = z1.ZLerpTo ( z2, dx, z1, z2 ).GetInterpolator ();
				var tLerp = t1.ZLerpTo ( t2, dx, z1, z2 ).GetInterpolator ();

				var xRasterizer = Rasterizer.Create <object> ( 0, w1, x1, x2, null,
					zLerp, tLerp );

				foreach ( var xSample in xRasterizer ) {
					int ix = xSample.Item1.PreciseRound ();
					double z = zLerp.Current;
					double2 t = tLerp.Current;

					double lod = Math.Log ( tpp * z, 2 );

					if ( ShowLods ) {
						// Grayscale lods
						//double intLod = ( double ) ( int ) lod / sampler.Map.NumLods;
						//double realLod = lod / sampler.Map.NumLods;

						//image.Values [ix, iy] = new double3 ( realLod, intLod, intLod );

						// Colored lods
						int intLod = ( int ) Math.Abs ( lod );

						if ( intLod != 0 ) {
							double lodMagnitude = 0.5;
							double3 lodColor = double3.Zero;
							
							if ( ( intLod & 1 ) == 1 )
								lodColor [0] = lodMagnitude;

							if ( ( intLod & 2 ) == 2 )
								lodColor [1] = lodMagnitude;

							if ( ( intLod & 4 ) == 4 )
								lodColor [2] = lodMagnitude;

							double3 c = sampler.GetColor ( t, lod );
							
							image.Values [ix, iy] = ( lodColor + c ) * 0.5;
						} else
							image.Values [ix, iy] = sampler.GetColor ( t, lod );
					} else
						image.Values [ix, iy] = sampler.GetColor ( t, lod );
				}
			}
		}
	}
}
